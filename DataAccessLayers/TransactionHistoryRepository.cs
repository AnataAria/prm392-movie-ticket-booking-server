using BusinessObjects;
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
            var transactionHistories =  await _context.SolvedTickets
                .Where(st => st.AccountId == accountId)
                .SelectMany(st => st.Transactions)
                .Select(t => new TransactionHistoryDto
                {
                    EventName = t.Event.Name,
                    TicketQuantity = t.SolvedTicket.Quantity,
                    TotalPrice = t.SolvedTicket.TotalPrice,
                    Time = t.TransactionHistories.FirstOrDefault().Time,
                    Status = t.Status,
                    TransactionType = t.Type.Name
                })
                .ToListAsync();

            return transactionHistories;
        }
    }
}
