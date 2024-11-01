using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class TicketRepository(Prn221projectContext context) : GenericRepository<Ticket>(context)
    {
        public async Task<int> GetRemainingTicketsForEvent(int eventId)
        {
            var totalTicketQuantity = await _context.Tickets
                .Where(t => t.MovieID == eventId)
                .SumAsync(t => t.Quantity ?? 0);

            var soldTicketQuantity = await _context.SolvedTickets
                .Where(st => st.Ticket!.MovieID == eventId)
                .SumAsync(st => st.Quantity ?? 0);

            var remainingTickets = totalTicketQuantity - soldTicketQuantity;
            return remainingTickets;
        }

        public async Task UpdateNew(Ticket ticket)
        {
            if (_context.Entry(ticket).State == EntityState.Detached)
            {
                _context.Tickets.Attach(ticket);
            }
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket?> GetByIdInclude(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Movie)
                .Include(t => t.Seat)
                .Include(t => t.Showtime)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            }

            return ticket;
        }

        public async Task<List<Ticket>> GetByMovieIdInclude(int movieId)
        {
            var tickets = await _context.Tickets
                .Include(t => t.Movie)
                .Include(t => t.Seat)
                .Include(t => t.Showtime) 
                .Where(t => t.MovieID == movieId) 
                .ToListAsync();  

            return tickets;
        }

        public async Task<IEnumerable<Ticket>> GetAllIncludeAsync()
        {
            return await _context.Tickets
                .Include(t => t.Movie)
                .Include(t => t.Seat)
                .Include(t => t.Showtime)
                .ToListAsync();
        }
    }
}
