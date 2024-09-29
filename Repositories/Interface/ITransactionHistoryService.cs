using BusinessObjects;
using BusinessObjects.Dtos.TransactionHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITransactionHistoryService : IGenericService<TransactionHistory>
    {
        Task<List<TransactionHistory>> GetTransactionHistoryByAccountId(int accountId);
        Task<List<TransactionHistoryDto>> GetAllTransactionHistoryByAccountId(int accountId);
    }
}
