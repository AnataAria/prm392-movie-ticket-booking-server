using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class TicketRepository : GenericRepository<Ticket>
    {
        public async Task<int> GetRemainingTicketsForEvent(int eventId)
        {
            var totalTicketQuantity = await _context.Tickets
                .Where(t => t.EventId == eventId)
                .SumAsync(t => t.Quantity ?? 0);

            var soldTicketQuantity = await _context.SolvedTickets
                .Where(st => st.Ticket!.EventId == eventId)
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
    }
}
