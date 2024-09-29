using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.SolvedTicket
{
    public class PurchaseTicketRequestDto
    {
        public List<Ticket> Tickets { get; set; }
        public Account Account { get; set; }
        public int Quantity { get; set; }
    }
}
