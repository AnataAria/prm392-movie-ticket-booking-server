using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<int?> CountQuantityPeopleJoinEvent(Movie eventName);
        Task<List<Ticket>> GetByEventIdAsync(int eventId);
        Task UpdateNewTicket(Ticket ticket);
    }
}
