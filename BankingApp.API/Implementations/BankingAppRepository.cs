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
            var result = await _bankAppContext.Accounts.AddAsync(accountObj);
            await _bankAppContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void SaveTransaction(BankTransaction transactionObj)
        {
            _bankAppContext.Accounts.FirstOrDefault(e => e.AccountId == transactionObj.AccountId)
                .AccountTrans.Add(transactionObj);

            //var result = await _bankAppContext.Accounts.FirstOrDefault(e => e.AccountId == transactionObj.AccountId)
            //    .AccountTrans.AddAsync(transactionObj);
            await _bankAppContext.SaveChangesAsync();
        }

        public async Task<BankAccount> GetBankAccount(int accountNo)
        {
            return await _bankAppContext.Accounts
                .FirstOrDefaultAsync(e => e.AccountNumber == accountNo);
        }

        //public List<BankAccount> GetBankAccounts()
        //{
        //using (var context = new BankContext())
        //{
        //    var list = context.Accounts
        //        .Include(a => a.AccountTrans)
        //        .ToList();
        //    return list;
        //}
        //}
    }
}