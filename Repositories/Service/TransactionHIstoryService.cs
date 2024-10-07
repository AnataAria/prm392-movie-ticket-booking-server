using BusinessObjects;
using BusinessObjects.Dtos.TransactionHistory;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;

namespace Services.Service
{
    public class TransactionHIstoryService(IUnitOfWork unitOfWork) : GenericService<TransactionHistory>(unitOfWork), ITransactionHistoryService
    {
        public async Task<List<TransactionHistory>> GetTransactionHistoryByAccountId(int accountId)
        {
            var transactionHistory = await _unitOfWork.TransactionHistoryRepository.FindAsync(a => a.Transaction!.SolvedTicket!.AccountId == accountId);
            return transactionHistory.ToList();
        }

        public async Task <List<TransactionHistoryDto>> GetAllTransactionHistoryByAccountId(int accountId)
        {
            return await _unitOfWork.TransactionHistoryRepository.GetAllTransactionHistoryByAccountId(accountId);
        }
    }
}
