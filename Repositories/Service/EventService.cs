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
    public class EventService(GenericRepository<Event> eventDAO, IUnitOfWork unitOfWork) : GenericService<Event>(unitOfWork), IEventService
    {
        private readonly GenericRepository<Event> _eventDAO = eventDAO;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<Event>> GetAllInclude()
        {
            return await _unitOfWork.EventRepository.GetAllInclude();
        }
        public async Task<IEnumerable<Event>> GetAllIncludeType()
        {
            return await _unitOfWork.EventRepository.GetAllIncludeType();
        }
    }
}
