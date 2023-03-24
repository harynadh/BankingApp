using BankingApp.Models;

namespace BankingApp.API
{
    public interface IBankingAppRepository
    {
        public Task<BankAccount> CreateAccount(BankAccount accountObj);
        public void SaveTransaction(BankTransaction transactionObj);
        public Task<BankAccount> GetBankAccount(int accountNo);
        //public List<BankAccount> GetBankAccounts();
    }
}
