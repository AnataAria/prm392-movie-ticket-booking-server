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
        Task<List<Ticket>> GetByMovieIdAsync(int eventId);
        Task UpdateNewTicket(Ticket ticket);
        Task<Ticket?> GetByIdInclude(int id);
        Task<IEnumerable<Ticket>> GetAllIncludeAsync();
        Task<List<Ticket>> GetByMovieIdInclude(int movieId);
    }
}
