using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace SaleTicketProject.Pages
{
    public class ViewProfileModel : PageModel
    {
        private readonly IAccountService _account;

        public ViewProfileModel(IAccountService account)
        {
            _account = account;
        }
        [BindProperty]
        public Account? Account { get; set; } = null;
        [BindProperty]
        public int AccountId { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public async Task<IActionResult> OnGet(int accountId)
        {
            Account = await _account.GetById(accountId);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            var existingAccount = await _account.GetById(AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }
            if(Password == null)
            {
                TempData["ErrorMessage"] = "Password is not null!";
                return RedirectToPage(new { accountId = existingAccount.Id });
            }
            existingAccount.Password = Password;
            await _account.Update(existingAccount);
            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToPage(new { accountId = existingAccount.Id });
        }

        public async Task<IActionResult> OnPostViewHistoryTransaction()
        {
            var existingAccount = await _account.GetById(AccountId);
            if (existingAccount == null)
            {
                return NotFound();
            }
            return RedirectToPage("ViewTransactionHIstory", new { accountId = existingAccount.Id });
        }
    }
}
