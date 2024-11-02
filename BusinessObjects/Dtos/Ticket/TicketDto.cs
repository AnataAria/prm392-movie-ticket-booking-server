using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Ticket
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int SeatID { get; set; }
        public string SeatName { get; set; }
        public string? MovieName { get; set; }
        public DateTime ShowDateTime { get; set; }

        public int? Price { get; set; }

        public byte? Status { get; set; }

        public int? Quantity { get; set; }
    }
}
