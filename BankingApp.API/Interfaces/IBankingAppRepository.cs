using BankingApp.Models;

namespace BankingApp.API
{
    public interface IBankingAppRepository
    {
        public Task<BankAccount> CreateAccount(BankAccount accountObj);
        public void SaveTransaction(BankTransaction transactionObj);
        public Task<BankAccount> GetBankAccount(Guid accountId);
        public Task<IEnumerable<BankAccount>> GetAllBankAccounts();
        public void DeleteAccount(BankAccount transactionObj);
    }
}
