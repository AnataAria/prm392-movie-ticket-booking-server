using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class EventRepository(Prn221projectContext context) : GenericRepository<Event>(context)
    {
        public async Task<IEnumerable<Event>> GetAllInclude()
        {
            return await _context.Events.Include(a => a.Sponsor).ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetAllIncludeType()
        {
            return await _context.Events.Include(a => a.Category).ToListAsync();
        }
    }
}
