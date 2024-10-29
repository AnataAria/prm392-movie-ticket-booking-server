using BusinessObjects;
using DataAccessLayers;
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
    public class MovieService(GenericRepository<Movie> eventDAO, IUnitOfWork unitOfWork) : GenericService<Movie>(unitOfWork), IMovieService
    {
        private readonly GenericRepository<Movie> _eventDAO = eventDAO;
        private readonly IUnitOfWork _unitOfWork;

        //public async Task<IEnumerable<Movie>> GetAllInclude()
        //{
        //    return await _unitOfWork.EventRepository.GetAllInclude();
        //}
        public async Task<IEnumerable<Movie>> GetAllIncludeType()
        {
            return await _unitOfWork.EventRepository.GetAllIncludeType();
        }
    }
}
