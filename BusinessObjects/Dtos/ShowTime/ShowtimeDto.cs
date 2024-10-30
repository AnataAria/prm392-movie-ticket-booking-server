using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.ShowTime
{
    public class ShowtimeDto
    {
        public int Id { get; set; }
        public DateTime ShowDateTime { get; set; }
        public string RoomName { get; set; }
    }
}
