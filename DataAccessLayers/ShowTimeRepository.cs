using BusinessObjects;
using BusinessObjects.Dtos.ShowTime;
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
        public async Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId)
        {
            return await _context.ShowTimes
                .Where(s => s.MovieID == movieId)
                .Include(s => s.CinemaRoom)
                .Select(s => new ShowtimeDto
                {
                    Id = s.Id,
                    ShowDateTime = s.ShowDateTime,
                    RoomName = s.CinemaRoom.RoomName
                })
                .ToListAsync();
        }

        public async Task<ShowTime?> GetByIdIncludeAsync(int id)
        {
            var showTime = await _context.ShowTimes
                .Include(t => t.Tickets)
                .FirstOrDefaultAsync(t => t.Id == id);
            return showTime;
        }
    }
}
