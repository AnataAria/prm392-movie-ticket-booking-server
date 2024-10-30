using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public string Image { get; set; }
        public byte Status { get; set; }
        public string DirectorName { get; set; }
        public string Description { get; set; }
        public List<string> Showtime { get; set; }
    }
}
