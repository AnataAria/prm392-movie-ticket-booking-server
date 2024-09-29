using BusinessObjects;
using DataAccessLayers.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;
using Services.Service;

namespace SaleTicketProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountService _accountRepository;
        private readonly IEventService _eventRepository;

        [BindProperty]
        public Account Account { get; set; }
        public List<Event> Events = new List<Event>();
        public IndexModel(ILogger<IndexModel> logger, IAccountService accountRepository, IEventService eventRepository)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _eventRepository = eventRepository;
        }

        public async Task OnGet(int ID)
        {
            Account = await _accountRepository.GetById(ID)!;
            var events = await _eventRepository.GetAllInclude();
            Events = events.Where(a => a.DateEnd > DateOnly.FromDateTime(DateTime.Now)).ToList();
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int AccountId { get; set; }

        public async Task<IActionResult> OnPostProperty()
        {
            Account = await _accountRepository.GetById(AccountId) ?? throw new Exception();
            return RedirectToPage("Property-details",new { Id = Id, accountId = Account.Id});
        }
        public IActionResult OnPostHome()
        {
            return RedirectToPage("Index",new {ID = Account.Id});
        }
        public IActionResult OnPostProperties()
        {
            return RedirectToPage("Properties", new { ID = Account.Id });
        }
        public IActionResult OnPostContact()
        {
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
        public IActionResult OnPostProfile()
        {

            if (Account == null)
            {
                return NotFound();
            }
            return RedirectToPage("ViewProfile", new { accountId = Account.Id });
        }
    }
}
