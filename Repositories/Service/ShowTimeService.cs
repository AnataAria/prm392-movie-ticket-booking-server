using BusinessObjects;
using DataAccessLayers.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ShowTimeService(IUnitOfWork unitOfWork) : GenericService<ShowTime>(unitOfWork), IShowTimeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<ShowTime>> GetShowtimesByMovieId(int movieId) => await _unitOfWork.ShowTimeRepository.GetShowtimesByMovieId(movieId);

    }
}
