using BusinessObjects;
using BusinessObjects.Dtos.TransactionHistory;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class TransactionHIstoryService : GenericService<TransactionHistory>, ITransactionHistoryService
    {
        private readonly GenericRepository<TransactionHistory> _transactionHistoryDAO;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionHIstoryService(GenericRepository<TransactionHistory> transactionHistoryDAO, IUnitOfWork unitOfWork) : base(transactionHistoryDAO)
        {
            _transactionHistoryDAO = transactionHistoryDAO;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TransactionHistory>> GetTransactionHistoryByAccountId(int accountId)
        {
            var transactionHistory = await _unitOfWork.TransactionHistoryDAO.FindAsync(a => a.Transaction!.SolvedTicket!.AccountId == accountId);
            return transactionHistory.ToList();
        }

        public async Task <List<TransactionHistoryDto>> GetAllTransactionHistoryByAccountId(int accountId)
        {
            return await _unitOfWork.TransactionHistoryDAO.GetAllTransactionHistoryByAccountId(accountId);
        }
    }
}
