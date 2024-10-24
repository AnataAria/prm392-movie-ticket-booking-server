using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class Seat
    {
        public int Id { get; set; }
        public string? SeatNumber { get; set; }
        public int CinemaRoomId { get; set; }
        public virtual CinemaRoom? CinemaRoom { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = [];
    }
}
