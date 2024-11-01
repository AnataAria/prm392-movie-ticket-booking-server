using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;

namespace Services.Service
{
    public class AccountService : GenericService<Account>, IAccountService
    {
        private readonly AccountRepository _accountDAOHigher;//example nang cao code

        public AccountService(AccountRepository accountDAOHigher, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _accountDAOHigher = accountDAOHigher;
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
               await _unitOfWork.AccountRepository.UpdateAsync(account);
               await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Account?> GetSystemAccountByEmailAndPassword(string email, string password)
        {
            return await _unitOfWork.AccountRepository.GetSystemAccountByAccountEmailAndPassword(email, password);
        }

        public async Task<Account?> GetAccountByIdIncludeAsync(int id) => await _unitOfWork.AccountRepository.GetAccountByIdIncludeAsync(id);
        public async Task<IEnumerable<Account>> GetAllIncludeAsync() => await _unitOfWork.AccountRepository.GetAllIncludeAsync();
    }
}
