using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class TicketService(GenericRepository<Ticket> ticketDAO, IUnitOfWork unitOfWork) : GenericService<Ticket>(unitOfWork), ITicketService
    {
        private readonly GenericRepository<Ticket> _ticketDAO = ticketDAO;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<int?> CountQuantityPeopleJoinEvent(Event eventName)
        {
            var quantity = eventName.TicketQuantity;
            var currentTicket = await _unitOfWork.TicketRepository.GetRemainingTicketsForEvent(eventName.Id);
            return _ = quantity - currentTicket;
        }

        public async Task<List<Ticket>> GetByEventIdAsync(int eventId)
        {
           var tickets =  await _unitOfWork.TicketRepository.FindAsync(a => a.EventId == eventId);
            return tickets.ToList();
        }

        public async Task UpdateNewTicket(Ticket ticket)
        {
            await _unitOfWork.TicketRepository.UpdateNew(ticket);
        }
    }
}
