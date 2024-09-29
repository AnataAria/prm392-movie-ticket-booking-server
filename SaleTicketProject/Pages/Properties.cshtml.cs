using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;
using Services.Service;

namespace SaleTicketProject.Pages
{
    public class PropertiesModel : PageModel
    {
        private readonly IEventService _eventRepository;
        private readonly IAccountService _accountRepository;

        public List<Event> Events = new List<Event>();
        public PropertiesModel(IEventService eventRepository, IAccountService accountRepository)
        {
            _accountRepository = accountRepository;
            _eventRepository = eventRepository;
        }

        [BindProperty]
        public Account Account { get; set; }
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
            Account =  await _accountRepository.GetById(AccountId) ?? throw new Exception();
            return RedirectToPage("Property-details", new { Id = Id , accountId = Account.Id });
        }
        public IActionResult OnPostHome()
        {
            Account = Account;
            return RedirectToPage("Index",new { ID = Account!.Id });
        }

        public IActionResult OnPostProperties()
        {
            Account = Account;
            return RedirectToPage(new { ID = Account!.Id });
        }
        public IActionResult OnPostContact()
        {
            Account = Account;
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
    }
}

