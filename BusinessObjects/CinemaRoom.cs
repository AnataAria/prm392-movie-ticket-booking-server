using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class CinemaRoom
    {
        public int Id { get; set; }
        public string? RoomName { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Seat> Seats { get; set; } = [];
        public virtual ICollection<ShowTime> ShowTimes { get; set; } = [];
    }
}
