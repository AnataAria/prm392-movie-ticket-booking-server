using BusinessObjects;
using BusinessObjects.Dtos.ShowTime;
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

        public async Task<List<ShowtimeDto>> GetShowtimesByMovieId(int movieId)
        {
            if (_unitOfWork.ShowTimeRepository == null)
            {
                throw new InvalidOperationException("ShowTimeRepository is not initialized.");
            }

            return await _unitOfWork.ShowTimeRepository.GetShowtimesByMovieId(movieId);
        }

        public async Task<ShowTime?> GetByIdIncludeAsync(int id) => await _unitOfWork.ShowTimeRepository.GetByIdIncludeAsync(id);

    }
}
