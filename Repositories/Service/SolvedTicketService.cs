using BusinessObjects;
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
    public class SolvedTicketService : GenericService<SolvedTicket>, ISolvedTicketService
    {
        private readonly GenericRepository<SolvedTicket> _solvedTicketDAO;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromotionService _promotionRepository;
        private readonly IAccountService _accountRepository;

        public SolvedTicketService(GenericRepository<SolvedTicket> solvedTicketDAO, IUnitOfWork unitOfWork, IPromotionService promotionRepository, IAccountService accountRepository) : base(solvedTicketDAO)
        {
            _solvedTicketDAO = solvedTicketDAO;
            _unitOfWork = unitOfWork;
            _promotionRepository = promotionRepository;
            _accountRepository = accountRepository;
        }

        public async Task PurchaseTickets(List<Ticket> tickets, Account account, int quantity)//promotion, quantity of ticket, transaction - transaction hítory
        {

            foreach (var ticket in tickets)
            {
                var promotion = new Promotion();
                promotion = await _promotionRepository.CheckDiscount(quantity);

                var databaseTicket = await _unitOfWork.TicketDAO.GetByIdAsync(ticket.Id);
                if (databaseTicket == null) throw new Exception("NOT FOUND!");
                if (databaseTicket.Quantity > 0)
                {
                    if (ticket.Status == 0) continue;
                    if ((double)quantity! * (double)databaseTicket.Price! > account.Wallet) throw new Exception("YOUR PRICE IN ACCOUNT NOT ENOUGH");
                    if ((double)quantity * (double)databaseTicket.Price <= account.Wallet)
                        await _accountRepository.MinusDebt(quantity, databaseTicket.Price, promotion.Discount, account);

                    databaseTicket.Quantity = databaseTicket.Quantity - quantity;
                    if (databaseTicket.Quantity == 0) databaseTicket.Status = 0;

                    var checkSoldTicket = await _unitOfWork.SolvedTicketDAO.GetAllAsync();
                    var countSoldTicket = checkSoldTicket.Count();
                    var totalPrice = (int?)((double)databaseTicket.Price! * (double)quantity! - (double)databaseTicket.Price * (double)quantity * promotion!.Discount);
                    var solvedTicket = new SolvedTicket
                    {
                        Id = countSoldTicket + 1,
                        AccountId = account.Id,
                        TicketId = ticket.Id,
                        Quantity = quantity,
                        TotalPrice = totalPrice,
                        PromotionId = promotion!.Id,
                    };
                    var yourSolvedTicket = await _unitOfWork.SolvedTicketDAO.AddAsync(solvedTicket);
                    await _unitOfWork.TicketDAO.UpdateAsync(databaseTicket);
                    await _unitOfWork.SaveChangesAsync();

                    var checkTransaction = await _unitOfWork.TransactionDAO.GetAllAsync();
                    var countTransaction = checkTransaction.Count();
                    var transaction = new Transaction
                    {
                        Id = countTransaction + 1,
                        EventId = databaseTicket.EventId,
                        SolvedTicketId = yourSolvedTicket.Id,
                        TypeId = 1,
                        Status = "Completed"
                    };
                    await _unitOfWork.TransactionDAO.AddAsync(transaction);
                    await _unitOfWork.SaveChangesAsync();

                    var checkTransactionHistory =  await _unitOfWork.TransactionHistoryDAO.GetAllAsync();
                    var countTransactionHistory = checkTransactionHistory.Count();
                    var transactionHistory = new TransactionHistory
                    {
                        Id = countTransactionHistory + 1,
                        TransactionId = transaction.Id,
                        Price = totalPrice,
                        Time = DateOnly.FromDateTime(DateTime.Now),
                        Status = "Completed"
                    };
                    await _unitOfWork.TransactionHistoryDAO.AddAsync(transactionHistory);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task<List<SolvedTicket>> GetSolvedTicketsByAccountId(int accountId)
        {
            var SolvedTicket = await _unitOfWork.SolvedTicketDAO.FindAsync(a => a.AccountId == accountId);
            return SolvedTicket.ToList();
        }

        public async Task<Boolean> CheckSolvedTicket(int ticketId)
        {
            var checksolvedTicket = await _unitOfWork.SolvedTicketDAO.FindAsync(a => a.TicketId == ticketId);
            if (checksolvedTicket.IsNullOrEmpty())
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
