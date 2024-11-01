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

        public async Task<Account?> GetSystemAccountByAccountEmail(string accountEmail)
        {
            return await _context.Accounts.Include(t => t.Role).SingleOrDefaultAsync(m => m.Email == accountEmail);
        }

        public async Task<Account?> GetSystemAccountByIdIncludeRole(int id) {
            return await _context.Accounts.Include(r => r.Role).SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Account?> GetAccountByIdIncludeAsync(int id)
        {
            return await _context.Accounts
                .Include(c => c.Role)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAllIncludeAsync()
        {
            return await _context.Accounts
                .Include(s => s.Role)
                .ToListAsync();
        }
    }
}
