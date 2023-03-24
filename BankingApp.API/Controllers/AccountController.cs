using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IBankingAppRepository _accountRepository;
        public AccountController(IBankingAppRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<ActionResult<BankAccount>> CreateAccount(BankAccount accountObj)
        {
            try
            {
                if (accountObj == null)
                    return BadRequest();

                var createdAcccount = await _accountRepository.CreateAccount(accountObj);

                return CreatedAtAction(nameof(GetBankAccount),
                    new { id = createdAcccount.AccountNumber }, createdAcccount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new account");
            }
        }

        [HttpPost]
        [Route("SaveTransaction")]
        public async Task<ActionResult<BankAccount>> SaveTransaction(BankTransaction transactionObj)
        {
            try
            {
                if (transactionObj == null)
                    return BadRequest();

                _accountRepository.SaveTransaction(transactionObj);
                return StatusCode(StatusCodes.Status200OK,
                    "Transaction saved successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new account");
            }
        }

        [HttpGet]
        [Route("GetBankAccount/{accountNumber}")]
        public ActionResult<List<BankAccount>> GetBankAccount(int accountNumber)
        {
            return Ok(_accountRepository.GetBankAccount(accountNumber));
        }
    }
}