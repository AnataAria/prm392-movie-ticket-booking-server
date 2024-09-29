using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public async Task<Category?> getByCateName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(a => a.Type == name);
        }
    }
}
