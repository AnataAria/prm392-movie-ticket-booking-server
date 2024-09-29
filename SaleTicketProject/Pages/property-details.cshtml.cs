using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Interface;

namespace SaleTicketProject.Pages
{
    public class Property_detailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITicketService _ticketRepository;
        private readonly IEventService _eventRepository;
        private readonly IAccountService _accountRepository;
        private readonly ICategoryService _categoryRepository;
        private readonly ISolvedTicketService _solvedTicketRepository;

        public Property_detailsModel(ILogger<IndexModel> logger, ITicketService ticketRepository, IEventService eventRepository, IAccountService accountRepository, ICategoryService categoryRepository, ISolvedTicketService solvedTicketRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _solvedTicketRepository = solvedTicketRepository;
        }
        [BindProperty]
        public Event Event { get; set; }
        [BindProperty]
        public List<Ticket> Ticket { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public Category Category { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public int Id { get; set; }
        public async Task<IActionResult> OnPostPropertyDetails()
        {
            if(Quantity == null || Quantity == 0 || Quantity < 0)
            {
                TempData["ErrorMessage"] = "Quantity cannot null or 0 lower 0!";
                return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
            }
            Account = await _accountRepository.GetById((int)Account.Id!);
            Event = await _eventRepository.GetById(Event.Id);
            Ticket = await _ticketRepository.GetByEventIdAsync(Event.Id);
            Category = await _categoryRepository.GetById((int)Event.CategoryId!);

            foreach(var ticket in Ticket)
            {
                if (ticket.Quantity == 0)
                {
                    TempData["ErrorMessage"] = "Ticket Sold Out!";
                    return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
                }
                if (ticket.Quantity < Quantity)
                {
                    TempData["WarningMessage"] = "Not Enough Ticket for you to buy!";
                    return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
                }
            }

            double? totalQUantity = 0;
            foreach (var ticket in Ticket)
            {
                totalQUantity = (double?)(Quantity * ticket.Price);
            }
            if(Account!.Wallet < totalQUantity)
            {
                TempData["WarningMessage"] = "Your wallet is not enough to buy tickets!";
                return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
            }
            await _solvedTicketRepository.PurchaseTickets(Ticket, Account, Quantity);
            TempData["SuccessMessage"] = "Buy tickets successfully!";
            return RedirectToPage(new { Id = Event.Id, accountId = Account.Id });
        }
        public async Task OnGet(int Id, int accountId)
        {
            Account = await _accountRepository.GetById((int)accountId!);
            Event = await _eventRepository.GetById(Id);
            Ticket = await _ticketRepository.GetByEventIdAsync(Id);
            Category = await _categoryRepository.GetById((int)Event.CategoryId!);
        }
        public async Task<IActionResult> OnPostHome()
        {
            Account = await _accountRepository.GetById((int)Account.Id!);
            return RedirectToPage("/Index",new { ID = Account.Id });
        }

        public async  Task<IActionResult> OnPostProperties()
        {
            Account = await _accountRepository.GetById((int)Account.Id!);
            return RedirectToPage("/Properties", new { ID = Account!.Id });
        }

        public IActionResult OnPostContact()
        {
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
    }
}
