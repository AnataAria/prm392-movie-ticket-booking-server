using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayers
{
    public class AccountRepository(Prn221projectContext context) : GenericRepository<Account>(context)
    {
        public async Task<List<Account>> GetAllName()
        {
            return await _context.Set<Account>().ToListAsync();
        }

        public async Task<Account?> GetSystemAccountByAccountEmailAndPassword(string accountEmail, string password)
        {
            return await _context.Accounts.SingleOrDefaultAsync(m => m.Email == accountEmail && m.Password == password);
        }
    }
}
