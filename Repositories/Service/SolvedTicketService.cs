﻿using BusinessObjects;
using BusinessObjects.Dtos.SolvedTicket;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class SolvedTicketService(IUnitOfWork unitOfWork, IPromotionService promotionRepository, IAccountService accountRepository) : GenericService<SolvedTicket>(unitOfWork), ISolvedTicketService
    {
        private readonly IPromotionService _promotionRepository = promotionRepository;
        private readonly IAccountService _accountRepository = accountRepository;

        public async Task<PurchaseTicketResponseDto> PurchaseTickets(int showtimeId, List<int> seatIds, Account account)
        {
            var showtime = await _unitOfWork.ShowTimeRepository.GetByIdIncludeAsync(showtimeId)
                ?? throw new Exception("Showtime not found");
            double totalTicketPrice = showtime.Tickets
                .Where(t => seatIds.Contains(t.SeatID) && t.Quantity > 0)
                .Sum(t => (double)t.Price!);

            if (totalTicketPrice > account.Wallet)
                throw new Exception("Insufficient account balance");
            await _accountRepository.MinusDebt(totalTicketPrice, account);

            var solvedTickets = new List<SolvedTicket>();
            var transactions = new List<Transaction>();
            var transactionHistories = new List<TransactionHistory>();

            foreach (var seatId in seatIds)
            {
                var ticket = showtime.Tickets.FirstOrDefault(t => t.SeatID == seatId && t.Quantity > 0)
                      ?? throw new Exception($"Ticket for seat {seatId} is unavailable");

                ticket.Quantity -= 1;
                if (ticket.Quantity == 0) ticket.Status = 0;

                solvedTickets.Add(new SolvedTicket
                {
                    AccountId = account.Id,
                    TicketId = ticket.Id,
                    Quantity = 1,
                    TotalPrice = (int)ticket.Price
                });
                //transactions.Add(new Transaction
                //{
                //    MovieID = showtime.MovieID,
                //    SolvedTicketId = solvedTickets.Last().Id,
                //    TypeId = 1,
                //    Status = "Completed"
                //});
                //transactionHistories.Add(new TransactionHistory
                //{
                //    TransactionId = transactions.Last().Id,
                //    Price = (int)totalTicketPrice,
                //    Time = DateOnly.FromDateTime(DateTime.Now),
                //    Status = "Completed"
                //});
            }

            await _unitOfWork.SolvedTicketRepository.AddRangeAsync(solvedTickets);
            await _unitOfWork.SaveChangesAsync();

            foreach (var solvedTicket in solvedTickets)
            {
                var transaction = new Transaction
                {
                    MovieID = showtime.MovieID,
                    SolvedTicketId = solvedTicket.Id,
                    TypeId = 1,
                    Status = "Completed"
                };
                transactions.Add(transaction);
            }
            await _unitOfWork.TransactionRepository.AddRangeAsync(transactions);
            await _unitOfWork.SaveChangesAsync();

            foreach (var transaction in transactions)
            {
                var transactionHistory = new TransactionHistory
                {
                    TransactionId = transaction.Id,
                    Price = (int)totalTicketPrice,
                    Time = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Completed"
                };
                transactionHistories.Add(transactionHistory);
            }
            await _unitOfWork.TransactionHistoryRepository.AddRangeAsync(transactionHistories);
            await _unitOfWork.SaveChangesAsync();

            return new PurchaseTicketResponseDto
            {
                TotalPrice = totalTicketPrice,
                MovieName = showtime.Movie.Name,
                ShowDateTime = showtime.ShowDateTime,
            };
        }


        public async Task<List<SolvedTicket>> GetSolvedTicketsByAccountId(int accountId)
        {
            var SolvedTicket = await _unitOfWork.SolvedTicketRepository.FindAsync(a => a.AccountId == accountId);
            return SolvedTicket.ToList();
        }

        public async Task<Boolean> CheckSolvedTicket(int ticketId)
        {
            var checksolvedTicket = await _unitOfWork.SolvedTicketRepository.FindAsync(a => a.TicketId == ticketId);
            if (checksolvedTicket == null || !checksolvedTicket.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
