using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace SaleTicketProject.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IAccountService _accountRepository;
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public int Id { get; set; }
        public ContactModel(IAccountService accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task OnGet(int ID)
        {
            Account = await _accountRepository.GetById(ID)!;
        }

        public IActionResult OnPostHome()
        {
            return RedirectToPage("Index", new { ID = Account.Id });
        }
        public IActionResult OnPostProperties()
        {
            return RedirectToPage("Properties", new { ID = Account.Id });
        }
        public IActionResult OnPostContact()
        {
            return RedirectToPage("Contact", new { ID = Account.Id });
        }
    }
}
