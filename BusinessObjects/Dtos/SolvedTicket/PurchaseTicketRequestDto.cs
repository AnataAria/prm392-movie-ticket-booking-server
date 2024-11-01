using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.SolvedTicket
{
    public class PurchaseTicketRequestDto
    {
        public int ShowtimeId { get; set; }
        public List<int> SeatIds { get; set; }
    }
}
