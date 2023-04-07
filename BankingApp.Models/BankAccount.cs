using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
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

        [DefaultValue(0)]
        public decimal? Balance { get; set; }

        public decimal? DepositAmount { get; set; }

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
        [DefaultValue(0)]
        public decimal? Deposit { get; set; }
        [DefaultValue(0)]
        public decimal? Withdraw { get; set; }
    }
}