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
        public AccountRepository AccountDAO { get; }
        public CategoryRepository CategoryDAO { get; }
        public EventRepository EventDAO { get; }
        public PromotionRepository PromotionDAO { get; }
        public RoleRepository RoleDAO { get; }
        public SolvedTicketRepository SolvedTicketDAO { get; }
        public TicketRepository TicketDAO { get; }
        public TransactionRepository TransactionDAO { get; }
        public TransactionHistoryRepository TransactionHistoryDAO { get; }
        public TransactionTypeRepository TransactionTypeDAO { get; }
        Task SaveChangesAsync();

    }
}
