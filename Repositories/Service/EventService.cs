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
    public class EventService : GenericService<Event>, IEventService
    {
        private readonly GenericRepository<Event> _eventDAO;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(GenericRepository<Event> eventDAO, IUnitOfWork unitOfWork) : base(eventDAO)
        {
            _eventDAO = eventDAO;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> GetAllInclude()
        {
            return await _unitOfWork.EventDAO.GetAllInclude();
        }
        public async Task<IEnumerable<Event>> GetAllIncludeType()
        {
            return await _unitOfWork.EventDAO.GetAllIncludeType();
        }
    }
}
