using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class SeatRepository(Prn221projectContext context) : GenericRepository<Seat>(context)
    {
        public async Task<IEnumerable<Seat>> GetAvailableSeatsByShowtimeId(int showtimeId)
        {
            var showtime = await _context.ShowTimes
                .Include(st => st.CinemaRoom)
                .ThenInclude(cr => cr.Seats)
                .FirstOrDefaultAsync(st => st.Id == showtimeId);

            if (showtime == null || showtime.CinemaRoom == null)
                throw new Exception("Showtime or associated Cinema Room not found");

            var availableSeats = showtime.CinemaRoom.Seats
                .Where(seat => seat.Tickets.All(ticket => ticket.ShowtimeID != showtimeId));

            return availableSeats;
        }
    }
}
