using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            BankAccounts = new List<BankAccount>()
        {
            new BankAccount(){ AccountNumber=1, AccountId=Guid.NewGuid(), Balance=  0, Name="Test",MobileNo="9985685689"}
        };
        }
    }
}