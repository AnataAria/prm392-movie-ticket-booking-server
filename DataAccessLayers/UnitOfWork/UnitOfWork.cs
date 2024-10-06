using BusinessObjects;

namespace DataAccessLayers.UnitOfWork
{
    public class UnitOfWork(Prn221projectContext context) : IUnitOfWork
    {
        private readonly Prn221projectContext _projectContext = context;
        private CategoryRepository categoryRepository;
        private AccountRepository accountRepository;
        private EventRepository eventRepository;
        private PromotionRepository promotionRepository;
        private RoleRepository roleRepository;
        private SolvedTicketRepository solvedTicketRepository;

        public AccountRepository AccountRepository
        {
            get
            {
                accountRepository ??= new AccountRepository(_projectContext);
                return accountRepository;
            }
        }

        
        public CategoryRepository CategoryRepository
        {
            get
            {
                categoryRepository ??= new CategoryRepository(_projectContext);

                return categoryRepository;
            }
        }

        
        public EventRepository EventRepository
        {
            get
            {
                eventRepository ??= new EventRepository(_projectContext);
                return eventRepository;
            }
        }

        
        public PromotionRepository PromotionRepository
        {
            get
            {
                promotionRepository ??= new PromotionRepository(_projectContext);

                return promotionRepository;
            }
        }

        
        public RoleRepository RoleRepository
        {
            get
            {
                roleRepository ??= new RoleRepository(_projectContext);

                return roleRepository;
            }
        }

        
        public SolvedTicketRepository SolvedTicketRepository
        {
            get
            {
                solvedTicketRepository ??= new SolvedTicketRepository(_projectContext);

                return solvedTicketRepository;
            }
        }

        private TicketRepository ticketRepository;
        public TicketRepository TicketRepository
        {
            get
            {
                ticketRepository ??= new TicketRepository(_projectContext);

                return ticketRepository;
            }
        }

        private TransactionRepository transactionRepository;
        public TransactionRepository TransactionRepository
        {
            get
            {
                transactionRepository ??= new TransactionRepository(_projectContext);

                return transactionRepository;
            }
        }

        private TransactionHistoryRepository transactionHistoryRepository;
        public TransactionHistoryRepository TransactionHistoryRepository
        {
            get
            {
                transactionHistoryRepository ??= new TransactionHistoryRepository(_projectContext);

                return transactionHistoryRepository;
            }
        }

        private TransactionTypeRepository transactionTypeRepository;
        public TransactionTypeRepository TransactionTypeRepository
        {
            get
            {
                transactionTypeRepository ??= new TransactionTypeRepository(_projectContext);

                return transactionTypeRepository;
            }
        }

        public GenericRepository<E> GenericRepository<E> () where E : class {
            return new GenericRepository<E>(_projectContext);
        }

        public async Task SaveChangesAsync()
        {
            await _projectContext.SaveChangesAsync();
        }
    }
}
