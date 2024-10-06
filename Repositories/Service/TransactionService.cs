using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;

namespace Services.Service
{
    public class TransactionService(IUnitOfWork unitOfWork) : GenericService<Transaction>(unitOfWork), ITransactionService
    {
    }
}
