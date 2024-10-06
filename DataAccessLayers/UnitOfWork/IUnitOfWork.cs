using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.UnitOfWork
{
    public interface IUnitOfWork
    {
        public AccountRepository AccountRepository { get; }
        public CategoryRepository CategoryRepository { get; }
        public EventRepository EventRepository { get; }
        public PromotionRepository PromotionRepository { get; }
        public RoleRepository RoleRepository { get; }
        public SolvedTicketRepository SolvedTicketRepository { get; }
        public TicketRepository TicketRepository { get; }
        public TransactionRepository TransactionRepository { get; }
        public TransactionHistoryRepository TransactionHistoryRepository { get; }
        public TransactionTypeRepository TransactionTypeRepository { get; }
        public GenericRepository<E> GenericRepository<E> () where E : class ;
        Task SaveChangesAsync();

    }
}
