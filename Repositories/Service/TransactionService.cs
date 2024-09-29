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
    public class TransactionService : GenericService<Transaction>, ITransactionService
    {
        private readonly GenericRepository<Transaction> _transactionDAO;

        public TransactionService(GenericRepository<Transaction> transactionDAO) : base(transactionDAO)
        {
            _transactionDAO = transactionDAO;
        }
    }
}
