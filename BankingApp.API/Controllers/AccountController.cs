using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
{
    //[Authorize]
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

                if (accountObj.DepositAmount > 0)
                {
                    BankTransaction transactionObj = new BankTransaction();
                    transactionObj.AccountId = createdAcccount.AccountId;
                    transactionObj.Deposit = accountObj.DepositAmount;
                    SaveTransaction(transactionObj);
                }

                return CreatedAtAction(nameof(GetBankAccount),
                    new { id = createdAcccount.AccountId }, createdAcccount);
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
        [Route("GetBankAccount/{accountId}")]
        public ActionResult<List<BankAccount>> GetBankAccount(Guid accountId)
        {
            return Ok(_accountRepository.GetBankAccount(accountId));
        }

        [HttpGet]
        [Route("GetAllBankAccounts")]
        public ActionResult<List<BankAccount>> GetAllBankAccounts()
        {
            return Ok(_accountRepository.GetAllBankAccounts());
        }

        [HttpPost]
        [Route("DeleteAccount")]
        public ActionResult<BankAccount> DeleteAccount(BankAccount accountObj)
        {
            try
            {
                _accountRepository.DeleteAccount(accountObj);
                return StatusCode(StatusCodes.Status200OK,
                    "Account deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting account");
            }
        }
    }
}