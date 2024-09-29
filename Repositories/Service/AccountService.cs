using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AccountService : GenericService<Account>, IAccountService
    {
        private readonly GenericRepository<Account> _accountDAO;
        private readonly AccountRepository _accountDAOHigher;//example nang cao code
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(GenericRepository<Account> accountDAO, AccountRepository accountDAOHigher, IUnitOfWork unitOfWork) : base(accountDAO)
        {
            _accountDAO = accountDAO;
            _accountDAOHigher = accountDAOHigher;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Account>> GetAllName()//vi du service nang cao
        {
            return await _accountDAOHigher.GetAllName();
        }

        public async Task MinusDebt(int? quantity, int? prize, double? discount, Account account)
        {
            double? accountPay = 0;
            if (quantity < 10) accountPay = account.Wallet! - (double)prize! * (double)quantity;
            else accountPay = account.Wallet! - ((double)prize! * (double)quantity! - (double)prize * (double)quantity * discount);

            if (accountPay >= 0)
            {
                account.Wallet = accountPay;
               await _unitOfWork.AccountDAO.UpdateAsync(account);
               await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Account?> GetSystemAccountByEmailAndPassword(string email, string password)
        {
            return await _unitOfWork.AccountDAO.GetSystemAccountByAccountEmailAndPassword(email, password);
        }
    }
}
