using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class ShowTimeRepository(Prn221projectContext context) : GenericRepository<ShowTime>(context)
    {
        public async Task<IEnumerable<ShowTime>> GetShowtimesByMovieId(int movieId)
        {
            return await _context.ShowTimes
                .Where(s => s.MovieID == movieId)
                .Include(s => s.CinemaRoom)
                .ToListAsync();
        }
    }
}
