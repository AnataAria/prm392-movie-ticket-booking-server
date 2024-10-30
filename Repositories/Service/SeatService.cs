using BusinessObjects;
using BusinessObjects.Dtos.Seat;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class SeatService(IUnitOfWork unitOfWork) : GenericService<Seat>(unitOfWork), ISeatService
    {

        public async Task<IEnumerable<SeatDto>> GetAvailableSeatsByShowtimeId(int showtimeId, int movieId) => await _unitOfWork.SeatRepository.GetAvailableSeatsByShowtimeId(showtimeId, movieId);
    }
}
