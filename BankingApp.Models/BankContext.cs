using BankingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace BankingApp.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
        : base(options)
        {
            //options.
        }

        //public BankContext(DbContextOptionsBuilder optionsBuilder)
        //: base()
        //{
        //    optionsBuilder.UseInMemoryDatabase(databaseName: "BankingAppDb");
        //}

        // protected override void OnConfiguring
        //(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseInMemoryDatabase(databaseName: "BankingAppDb");
        // }

        public DbSet<BankAccount> Accounts { get; set; }
        public DbSet<BankTransaction> AccountTrans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Accountss Table
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount { AccountId = Guid.NewGuid(), Name = "Hari" });
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount { AccountId = Guid.NewGuid(), Name = "Ravi" });
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount { AccountId = Guid.NewGuid(), Name = "Amit" });
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount { AccountId = Guid.NewGuid(), Name = "Jitin" });

            // Seed Employee Table
            modelBuilder.Entity<BankAccount>().HasData(new BankAccount
            {
                AccountId = Guid.NewGuid(),
                Name = "Hari",
                MobileNo = "9989712169",
                AccountNumber = 1011,
                AccountType = "Savings"
            });

            modelBuilder.Entity<BankAccount>().HasData(new BankAccount
            {
                AccountId = Guid.NewGuid(),
                Name = "Ravi",
                MobileNo = "9195698789",
                AccountNumber = 1012,
                AccountType = "Savings"
            });

            modelBuilder.Entity<BankAccount>().HasData(new BankAccount
            {
                AccountId = Guid.NewGuid(),
                Name = "Amit",
                MobileNo = "9687458969",
                AccountNumber = 1013,
                AccountType = "Savings"
            });

            modelBuilder.Entity<BankAccount>().HasData(new BankAccount
            {
                AccountId = Guid.NewGuid(),
                Name = "Jitin",
                MobileNo = "9581523689",
                AccountNumber = 1014,
                AccountType = "Savings"
            });
        }
    }
}