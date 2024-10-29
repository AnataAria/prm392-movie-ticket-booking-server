using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class MovieRepository(Prn221projectContext context) : GenericRepository<Movie>(context)
    {
        public async Task<IEnumerable<Movie>> GetAllInclude()
        {
            return await _context.Events.Include(a => a.Category).ToListAsync();
        }
        public async Task<IEnumerable<Movie>> GetAllIncludeType()
        {
            return await _context.Events.Include(a => a.Category).ToListAsync();
        }
    }
}
