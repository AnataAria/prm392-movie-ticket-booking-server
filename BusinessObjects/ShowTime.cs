using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class ShowTime
    {
        public int Id { get; set; }
        public int CinemaRoomID { get; set; }
        public int MovieID { get; set; }
        public int TicketQuantity { get; set; }
        public DateTime ShowDateTime { get; set; }
        public int AvaliableSeats { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual CinemaRoom? CinemaRoom { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = [];
    }
}
