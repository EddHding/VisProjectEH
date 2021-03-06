﻿using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Accounting.Model;

namespace Accounting.DataBase
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(string dbName) : base(dbName)
        {
            Database.SetInitializer(new AccountingDbInitializer());
        }

        public DbSet<BalanceSheet> BalanceSheets { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SaleTransaction> SaleTransactions { get; set; }
        public DbSet<ProfitLossStatement> ProfitLossStatements { get; set; }
        public DbSet<AuditRecord> AuditRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DefineAccount(modelBuilder.Entity<Account>());
            DefineAbstractTransaction(modelBuilder.Entity<AbstractTransaction>());
            //DefineBalance(modelBuilder.Entity<Balance>());
            //DefineBalanceSheet(modelBuilder.Entity<BalanceSheet>());
            //DefineAbstractTransaction(modelBuilder.Entity<AbstractTransaction>());

        }

        private void DefineBalanceSheet(EntityTypeConfiguration<BalanceSheet> bs)
        {
            throw new NotImplementedException();
        }

        private void DefineBalance(EntityTypeConfiguration<Balance> b)
        {
            throw new NotImplementedException();
        }

        private void DefineAbstractTransaction(EntityTypeConfiguration<AbstractTransaction> atConfig)
        {
            atConfig.ToTable("Transaction Record");
            atConfig.HasKey(at => at.Id);
            atConfig.Property(at => at.Date).HasColumnName("DATE OF TRANSACTION");
            atConfig.Property(at => at.Amount).HasColumnName("AMOUNT");
        }

        private void DefineAccount(EntityTypeConfiguration<Account> accountConfig)
        {
            accountConfig.ToTable("Account Record");
            accountConfig.HasKey(a => a.Id);
            accountConfig.Property(a => a.AccountName).HasColumnName("ACCOUNT NAME");
            accountConfig.Property(a => a.TypeOfAccount).HasColumnName("TYPE OF ACCOUNT");
        }

        private void DefineProfitLossStatement(EntityTypeConfiguration<ProfitLossStatement> plConfig)
        {
            plConfig.ToTable("Profit and Loss Statement");
            plConfig.HasKey(a => a.Id);
            plConfig.Property(a => a.StartDate).HasColumnName("START DATE");
            plConfig.Property(a => a.EndDate).HasColumnName("END DATE");
        }
    }

}
