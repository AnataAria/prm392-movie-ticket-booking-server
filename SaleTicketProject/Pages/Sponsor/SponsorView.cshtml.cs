using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace SaleTicketProject.Pages.Sponsor
{
    public class SponsorViewModel : PageModel
    {
        private readonly IEventService _eventRepository;
        private readonly IAccountService _accountRepository;
        private readonly ITicketService _ticketRepository;
        private readonly ISolvedTicketService _solvedTicketRepository;
        private readonly IRoleService _roleRepository;

        public List<Event> Events = new List<Event>();
        public SponsorViewModel(IEventService eventRepository, IAccountService accountRepository, ITicketService ticketRepository, ISolvedTicketService solvedTicketRepository, IRoleService roleRepository)
        {
            _accountRepository = accountRepository;
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _solvedTicketRepository = solvedTicketRepository;
            _roleRepository = roleRepository;
        }

        [BindProperty]
        public Account Account { get; set; }

        [BindProperty]
        public Role RoleName { get; set; }
        public async Task OnGet(int ID)
        {

            Account = await _accountRepository.GetById(ID)!;
            RoleName = await _roleRepository.GetById((int)Account.RoleId);
            var events = await _eventRepository.GetAll();
            Events = events.Where(a => a.DateStart > DateOnly.FromDateTime(DateTime.Now)).ToList();
        }

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int AccountId { get; set; }
        [BindProperty]
        public string ServiceSponsor {  get; set; }

        public async Task<IActionResult> OnPostProperty()
        {
            Account = await _accountRepository.GetById(AccountId) ?? throw new Exception();
            return RedirectToPage("Property-details", new { Id = Id, accountId = Account.Id });
        }
        public IActionResult OnPostHome()
        {
            Account = Account;
            return RedirectToPage("Index", new { ID = Account!.Id });
        }

        public IActionResult OnPostProperties()
        {
            Account = Account;
            return RedirectToPage(new { ID = Account!.Id });
        }

        public async Task<IActionResult> OnPostSponsor()
        {
            var newEvent = await _eventRepository.GetById(Id) ?? throw new Exception();
            Account = await _accountRepository.GetById(AccountId) ?? throw new Exception();
            if(newEvent.SponsorId != null)
            {
                TempData["Message"] = "Event is already sponsored by ANOTHER SPONSOR!";
                return RedirectToPage(new { ID = Account.Id });
            }
            newEvent.SponsorId = Account.Id;
            newEvent.ServiceSponsor = ServiceSponsor;
            await _eventRepository.Update(newEvent);
            TempData["SuccessMessage"] = "Sponsor event successfully!";
            return RedirectToPage(new { ID = Account.Id });
        }

        public async Task<IActionResult> OnPostNotSponsor()
        {
            var newEvent = await _eventRepository.GetById(Id) ?? throw new Exception();
            Account = await _accountRepository.GetById(AccountId) ?? throw new Exception();
            var ticket = await _ticketRepository.GetByEventIdAsync(newEvent.Id);
            Boolean checkSolvedTicket = false;
            foreach (var checkticket in ticket)
            {
                checkSolvedTicket = await _solvedTicketRepository.CheckSolvedTicket(checkticket.Id);
            }
            if (checkSolvedTicket is false)
            {
                newEvent.SponsorId = null;
                newEvent.ServiceSponsor = null;
                await _eventRepository.Update(newEvent);
                TempData["SuccessMessage"] = "Disable Sponsor event successfully!";
                return RedirectToPage(new { ID = Account.Id });
            }
            else
            {
                TempData["Message"] = "This event's tickets have been bought, cannot take back!";
                return RedirectToPage(new { ID = Account.Id });
            }

        }
    }
}
