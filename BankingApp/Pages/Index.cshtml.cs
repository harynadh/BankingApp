using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace BankingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration conf)
        {
            _logger = logger;
            this._config = conf;
        }

        public void OnGet()
        {
            try
            {
                var client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
                var request = new RestRequest("GetAllBankAccounts");

                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter());
                options.PropertyNameCaseInsensitive = true;

                var response = client.Get(request);
                var data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(response.Content, options);
                BankAccounts = data["result"].Deserialize<List<BankAccount>>(options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<PartialViewResult> OnGetAccountAsync(string frm, Guid id)
        {
            var client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
            var request = new RestRequest("GetBankAccount/{accountId}")
                .AddUrlSegment("accountId", id);
            request.Method = Method.Get;
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNameCaseInsensitive = true;

            var response = client.Get(request);
            var data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(response.Content, options);

            if (frm == "depst")
                return Partial("_Deposit", data["result"].Deserialize<BankAccount>(options));
            else if (frm == "wdraw")
                return Partial("_Withdraw", data["result"].Deserialize<BankAccount>(options));
            else
                return Partial("_Delete", data["result"].Deserialize<BankAccount>(options));
        }

        public IActionResult OnPostDeleteModalPartial(BankAccount accountObj)
        {
            try
            {
                var client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
                var request = new RestRequest("DeleteAccount");
                request.Method = Method.Post;
                BankTransaction transObj = new BankTransaction();
                transObj.AccountId = accountObj.AccountId;
                request.AddBody(transObj);
                var response = client.ExecutePost(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostTransactionModalPartial(BankAccount accountObj)
        {
            try
            {
                var client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
                var request = new RestRequest("SaveTransaction");
                request.Method = Method.Post;
                BankTransaction transObj = new BankTransaction();
                transObj.AccountId = accountObj.AccountId;
                transObj.Deposit = (accountObj.AccountTrans.FirstOrDefault().Deposit == null) ? 0 : accountObj.AccountTrans.FirstOrDefault().Deposit;
                transObj.Withdraw = (accountObj.AccountTrans.FirstOrDefault().Withdraw == null) ? 0 : accountObj.AccountTrans.FirstOrDefault().Withdraw;
                request.AddBody(transObj);
                var response = client.ExecutePost(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToPage("/Index");
        }
    }
}