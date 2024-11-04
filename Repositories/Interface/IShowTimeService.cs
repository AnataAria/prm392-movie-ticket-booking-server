using BusinessObjects;
using BusinessObjects.Dtos.ShowTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IShowTimeService : IGenericService<ShowTime>
    {
        Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId);

        Task<ShowTime?> GetByIdIncludeAsync(int id);
    }
}
