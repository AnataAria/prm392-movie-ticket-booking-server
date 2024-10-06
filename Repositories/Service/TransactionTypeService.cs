using BusinessObjects;
using DataAccessLayers;
using Services.Interface;
using System;
using System.Collections.Generic;
using DataAccessLayers.UnitOfWork;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class TransactionTypeService(IUnitOfWork unitOfWork) : GenericService<TransactionType>(unitOfWork), ITransactionTypeService
    {
    }
}
