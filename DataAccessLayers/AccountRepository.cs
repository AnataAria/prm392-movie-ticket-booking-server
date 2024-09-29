using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class AccountRepository : GenericRepository<Account>
    {
        // co cach code gọi DAO của rieng model moi model
        // cach: chuyen private thanh public của context o genericDAO và dung _context cua genericDAO

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
