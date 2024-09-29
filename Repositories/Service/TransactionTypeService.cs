using BusinessObjects;
using DataAccessLayers;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class TransactionTypeService : GenericService<TransactionType>, ITransactionTypeService
    {
        private readonly GenericRepository<TransactionType> _transactionTypeDAO;

        public TransactionTypeService(GenericRepository<TransactionType> transactionTypeDAO) : base(transactionTypeDAO)
        {
            _transactionTypeDAO = transactionTypeDAO;
        }
    }
}
