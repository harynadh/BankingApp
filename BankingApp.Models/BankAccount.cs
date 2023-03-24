using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class BankAccount
    {
        [Key]
        public Guid AccountId { get; set; }
        public int AccountNumber { get; set; }
        public string? Name { get; set; }
        public string? AccountType { get; set; }
        public string? MobileNo { get; set; }
        public decimal? Balance { get; set; }
        public BankAccount()
        {
            AccountTrans = new HashSet<BankTransaction>();
        }
        public ICollection<BankTransaction> AccountTrans { get; set; }
    }

    public class BankTransaction
    {
        [Key]
        public int TransId { get; set; }
        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Withdraw { get; set; }
    }
}