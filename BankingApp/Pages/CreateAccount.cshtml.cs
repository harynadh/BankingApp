using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingApp.Pages
{
    public class CreateAccount : PageModel
    {
        private readonly ILogger<CreateAccount> _logger;

        public CreateAccount(ILogger<CreateAccount> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}