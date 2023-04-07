using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Text;
using System.Text.Json;

namespace BankingApp.Pages
{
    public class Withdraw : PageModel
    {
        private readonly ILogger<Withdraw> _logger;
        private HttpClient _client;

        public Withdraw(ILogger<Withdraw> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5024/api/account/");
        }

        public void OnGet()
        {

        }

        public void OnPostSubmit(BankAccount accountObj)
        {
            try
            {
                if (accountObj is null)
                {
                    _logger.LogError("Account object sent from client is null.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            var client = new RestClient("http://localhost:5024/api/account/");
            var request = new RestRequest("CreateAccount");
            request.Method = Method.Post;
            request.AddBody(accountObj);
            var response = client.ExecutePost(request);
        }
    }
}