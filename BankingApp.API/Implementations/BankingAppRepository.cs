using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankingApp.API
{
    public class BankingAppRepository : IBankingAppRepository
    {
        private readonly BankContext _bankAppContext;

        public BankingAppRepository(BankContext bankAppContext)
        {
            _bankAppContext = bankAppContext;
        }

        public async Task<BankAccount> CreateAccount(BankAccount accountObj)
        {
            if (_bankAppContext.Accounts.Count() == 0)
                accountObj.AccountNumber = int.Parse(DateTime.Now.ToString("yyyyMM") + "01");
            else
                accountObj.AccountNumber = accountObj.AccountNumber + _bankAppContext.Accounts.Max(b => b.AccountNumber) + 1;
            var result = await _bankAppContext.Accounts.AddAsync(accountObj);
            await _bankAppContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void SaveTransaction(BankTransaction transactionObj)
        {
            var account = _bankAppContext.Accounts.Include(b => b.AccountTrans).First(e => e.AccountId == transactionObj.AccountId);
            account.AccountTrans.Add(transactionObj);

            _bankAppContext.Accounts.FirstOrDefault(e => e.AccountId == transactionObj.AccountId)
                .Balance = (_bankAppContext.Accounts.FirstOrDefault(e => e.AccountId == transactionObj.AccountId).AccountTrans.Sum(t => t.Deposit)
                - _bankAppContext.Accounts.FirstOrDefault(e => e.AccountId == transactionObj.AccountId).AccountTrans.Sum(t => t.Withdraw));

            await _bankAppContext.SaveChangesAsync();
        }

        public async Task<BankAccount> GetBankAccount(Guid accountId)
        {
            return await _bankAppContext.Accounts
                .FirstOrDefaultAsync(e => e.AccountId == accountId);
        }

        public async Task<IEnumerable<BankAccount>> GetAllBankAccounts()
        {
            return await _bankAppContext.Accounts.ToListAsync();
        }

        public void DeleteAccount(BankAccount accountObj)
        {
            var result = _bankAppContext.Accounts.Remove(accountObj);
            _bankAppContext.SaveChangesAsync();
        }
    }
}