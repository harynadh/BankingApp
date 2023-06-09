﻿using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Text;
using System.Text.Json;

namespace BankingApp.Pages
{
    public class CreateAccount : PageModel
    {
        private readonly ILogger<CreateAccount> _logger;
        private readonly IConfiguration _config;

        public CreateAccount(ILogger<CreateAccount> logger, IConfiguration conf)
        {
            _logger = logger;
            this._config = conf;
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

                RestClient client = new RestClient(_config.GetValue<string>("WebAPIUrl"));
                var request = new RestRequest("CreateAccount");
                request.Method = Method.Post;
                request.AddBody(accountObj);
                var response = client.ExecutePost(request);
                Response.Redirect("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}