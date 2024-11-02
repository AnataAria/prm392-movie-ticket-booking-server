using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.SolvedTicket
{
    public class PurchaseTicketResponseDto
    {
        public double TotalPrice { get; set; }
        public string? MovieName { get; set; }
        public DateTime ShowDateTime { get; set; }
    }
}
