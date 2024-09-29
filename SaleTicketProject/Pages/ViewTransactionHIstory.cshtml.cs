using BusinessObjects;
using BusinessObjects.Dtos.TransactionHistory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;

namespace SaleTicketProject.Pages
{
    public class ViewTransactionHIstoryModel : PageModel
    {
        private readonly IAccountService _account;
        private readonly ITransactionHistoryService _transactionHistoryRepository;

        public ViewTransactionHIstoryModel(IAccountService account, ITransactionHistoryService transactionHistoryRepository)
        {
            _account = account;
            _transactionHistoryRepository = transactionHistoryRepository;
        }
        [BindProperty]
        public Account? Account { get; set; } = null;
        [BindProperty]
        public List<TransactionHistoryDto> TransactionHistory { get; set; }
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
            TransactionHistory =await _transactionHistoryRepository.GetAllTransactionHistoryByAccountId(accountId);
            return Page();
        }
    }
}
