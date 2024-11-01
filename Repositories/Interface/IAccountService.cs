using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAccountService : IGenericService<Account>
    {
        Task<List<Account>> GetAllName();
        Task MinusDebt(int? quantity, int? prize, double? discount, Account account);
        Task<Account?> GetSystemAccountByEmailAndPassword(string email, string password);
        Task<Account?> GetAccountByIdIncludeAsync(int id);
        Task<IEnumerable<Account>> GetAllIncludeAsync();
    }
}
