using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace BankingApp.Pages
{
    public class DepositTransaction : PageModel
    {
        private readonly ILogger<DepositTransaction> _logger;
        private readonly IConfiguration _config;
        public BankAccount bankAccount { get; set; }

        public DepositTransaction(ILogger<DepositTransaction> logger, IConfiguration conf)
        {
            _logger = logger;
            this._config = conf;
        }

        public void OnGet(Guid accountId)
        {
            var client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
            var request = new RestRequest("GetBankAccount/{accountId}")
                .AddUrlSegment("accountId", accountId);
            request.Method = Method.Get;
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNameCaseInsensitive = true;

            var response = client.Get(request);
            var data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(response.Content, options);
            bankAccount = data["result"].Deserialize<BankAccount>(options);
        }

        public IActionResult OnPostSubmit(BankAccount accountObj)
        {
            var client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
            var request = new RestRequest("SaveTransaction");
            request.Method = Method.Post;
            BankTransaction transObj = new BankTransaction();
            transObj.AccountId = accountObj.AccountId;
            transObj.Deposit = accountObj.AccountTrans.FirstOrDefault().Deposit;
            request.AddBody(transObj);
            var response = client.ExecutePost(request);
            return RedirectToPage("/Index");
            //Response.Redirect("/Index");
        }
    }
}