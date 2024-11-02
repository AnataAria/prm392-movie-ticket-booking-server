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

        public async Task MinusDebt(double totalAmount, Account account)
        {
            if (account.Wallet < totalAmount)
                throw new Exception("Insufficient funds");

            account.Wallet -= totalAmount;

            await _unitOfWork.AccountRepository.UpdateAsync(account);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Account?> GetSystemAccountByEmailAndPassword(string email, string password)
        {
            return await _unitOfWork.AccountRepository.GetSystemAccountByAccountEmailAndPassword(email, password);
        }

        public async Task<Account?> GetAccountByIdIncludeAsync(int id) => await _unitOfWork.AccountRepository.GetAccountByIdIncludeAsync(id);
        public async Task<IEnumerable<Account>> GetAllIncludeAsync() => await _unitOfWork.AccountRepository.GetAllIncludeAsync();
    }
}
