using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IShowTimeService : IGenericService<ShowTime>
    {
        Task<IEnumerable<ShowTime>> GetShowtimesByMovieId(int movieId);
    }
}
