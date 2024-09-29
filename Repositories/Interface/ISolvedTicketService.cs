using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISolvedTicketService : IGenericService<SolvedTicket>
    {
        Task PurchaseTickets(List<Ticket> tickets, Account account, int quantity);
        Task<List<SolvedTicket>> GetSolvedTicketsByAccountId(int accountId);
        Task<Boolean> CheckSolvedTicket(int ticketId);
    }
}
