using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Prn221projectContext _projectContext;
        public UnitOfWork()
        {
            _projectContext = new Prn221projectContext();
        }

        private AccountRepository accountDAO;
        public AccountRepository AccountDAO
        {
            get
            {
                if (accountDAO == null)
                {
                    accountDAO = new AccountRepository();
                }

                return accountDAO;
            }
        }

        private CategoryRepository categoryDAO;
        public CategoryRepository CategoryDAO
        {
            get
            {
                if (categoryDAO == null)
                {
                    categoryDAO = new CategoryRepository();
                }

                return categoryDAO;
            }
        }

        private EventRepository eventDAO;
        public EventRepository EventDAO
        {
            get
            {
                if (eventDAO == null)
                {
                    eventDAO = new EventRepository();
                }

                return eventDAO;
            }
        }

        private PromotionRepository promotionDAO;
        public PromotionRepository PromotionDAO
        {
            get
            {
                if (promotionDAO == null)
                {
                    promotionDAO = new PromotionRepository();
                }

                return promotionDAO;
            }
        }

        private RoleRepository roleDAO;
        public RoleRepository RoleDAO
        {
            get
            {
                if (roleDAO == null)
                {
                    roleDAO = new RoleRepository();
                }

                return roleDAO;
            }
        }

        private SolvedTicketRepository solvedTicketDAO;
        public SolvedTicketRepository SolvedTicketDAO
        {
            get
            {
                if (solvedTicketDAO == null)
                {
                    solvedTicketDAO = new SolvedTicketRepository();
                }

                return solvedTicketDAO;
            }
        }

        private TicketRepository ticketDAO;
        public TicketRepository TicketDAO
        {
            get
            {
                if (ticketDAO == null)
                {
                    ticketDAO = new TicketRepository();
                }

                return ticketDAO;
            }
        }

        private TransactionRepository transactionDAO;
        public TransactionRepository TransactionDAO
        {
            get
            {
                if (transactionDAO == null)
                {
                    transactionDAO = new TransactionRepository();
                }

                return transactionDAO;
            }
        }

        private TransactionHistoryRepository transactionHistoryDAO;
        public TransactionHistoryRepository TransactionHistoryDAO
        {
            get
            {
                if (transactionHistoryDAO == null)
                {
                    transactionHistoryDAO = new TransactionHistoryRepository();
                }

                return transactionHistoryDAO;
            }
        }

        private TransactionTypeRepository transactionTypeDAO;
        public TransactionTypeRepository TransactionTypeDAO
        {
            get
            {
                if (transactionTypeDAO == null)
                {
                    transactionTypeDAO = new TransactionTypeRepository();
                }

                return transactionTypeDAO;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _projectContext.SaveChangesAsync();
        }
    }
}
