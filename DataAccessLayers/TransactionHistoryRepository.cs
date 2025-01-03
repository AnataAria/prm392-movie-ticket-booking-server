﻿using BusinessObjects;
using BusinessObjects.Dtos.TransactionHistory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class TransactionHistoryRepository : GenericRepository<TransactionHistory>
    {
        public TransactionHistoryRepository(Prn221projectContext context) : base(context)
        {
        }

        public async Task<List<TransactionHistoryDto>> GetAllTransactionHistoryByAccountId(int accountId)
        {
            var transactionHistories = await _context.SolvedTickets
                .Where(st => st.AccountId == accountId)
                .SelectMany(st => st.Transactions)
                .Select(t => new
                {
                    MovieName = t.SolvedTicket.Ticket.Movie.Name,
                    ShowTime = t.SolvedTicket.Ticket.Showtime.ShowDateTime,
                    Room = t.SolvedTicket.Ticket.Showtime.CinemaRoom.RoomName,
                    SeatName = t.SolvedTicket.Ticket.Seat.SeatNumber,
                    TicketQuantity = t.SolvedTicket.Quantity,
                    TotalPrice = t.SolvedTicket.TotalPrice,
                    Time = t.TransactionHistories.FirstOrDefault().Time.Value.ToDateTime(new TimeOnly(0, 0)),
                    Status = t.Status,
                    TransactionType = t.Type.Name
                })
                .ToListAsync();

            var formattedTransactionHistories = transactionHistories
                .Select(t => new TransactionHistoryDto
                {
                    MovieName = t.MovieName,
                    ShowDateTime = t.ShowTime is DateTime dateTime1 ? dateTime1.ToString("yyyy/MM/dd HH:mm:ss") : string.Empty,
                    Room = t.Room,
                    SeatName = t.SeatName,
                    TicketQuantity = t.TicketQuantity,
                    TotalPrice = t.TotalPrice,
                    Time = t.Time is DateTime dateTime ? dateTime.ToString("yyyy/MM/dd HH:mm:ss") : string.Empty,
                    Status = t.Status,
                    TransactionType = t.TransactionType
                })
                .OrderByDescending(t => DateTime.TryParse(t.Time, out var parsedTime) ? parsedTime : DateTime.MinValue)
                .ToList();

            return formattedTransactionHistories;
        }

    }
}
