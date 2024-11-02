using BusinessObjects;
using BusinessObjects.Dtos.SolvedTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISolvedTicketService : IGenericService<SolvedTicket>
    {
        Task<PurchaseTicketResponseDto> PurchaseTickets(int showtimeId, List<int> seatIds, Account account);
        Task<List<SolvedTicket>> GetSolvedTicketsByAccountId(int accountId);
        Task<Boolean> CheckSolvedTicket(int ticketId);
    }
}
