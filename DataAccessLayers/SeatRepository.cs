using BusinessObjects;
using BusinessObjects.Dtos.Seat;
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
        public async Task<IEnumerable<SeatDto>> GetAvailableSeatsByShowtimeId(int showtimeId, int movieId)
        {
            var showtime = await _context.ShowTimes
                .Include(st => st.CinemaRoom)
                .ThenInclude(cr => cr.Seats)
                .FirstOrDefaultAsync(st => st.Id == showtimeId);

            if (showtime == null || showtime.CinemaRoom == null)
                return null;

            var bookedSeats = await _context.Tickets
                .Where(ticket => ticket.ShowtimeID == showtimeId && ticket.MovieID == movieId)
                .Select(ticket => ticket.SeatID)
                .ToListAsync();

            var availableSeats = showtime.CinemaRoom.Seats
                .Where(seat => bookedSeats.Contains(seat.Id))
                .Select(seat => new SeatDto
                {
                    Id = seat.Id,
                    SeatNumber = seat.SeatNumber,
                    CinemaRoomName = showtime.CinemaRoom.RoomName
                });

            return availableSeats.ToList();
        }
    }
}
