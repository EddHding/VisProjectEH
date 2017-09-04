using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Accounting.Model;
using System;

namespace Accounting.DataBase
{
    public class AccountingDbInitializer : DropCreateDatabaseAlways<AccountingDbContext>
    {
        private AccountingDbContext Context;
        protected override void Seed(AccountingDbContext context)
        {
            this.Context = context;
            //CreateFixture1(context); //Book example p.76 exhibit 8.2
            CreateFixture2(context); //Original data
        }

        private void AddNewBalanceSheet(DateTime date)
        {
            var bs = new BalanceSheet { Date = date};
            Context.BalanceSheets.Add(bs);
        }

        private Account AddNewAccount(string name, AccountType type)
        {
            var ac = new Account() { AccountName = name, TypeOfAccount = type };
            Context.Accounts.Add(ac);
            return ac;
        }

        private void AddBalancetoAccount(Account account, decimal amount)
        {
            //account.balance = amount;
        }

        private Transaction AddNewTransaction(string name, Account dAccount, Account cAccount, DateTime date, decimal amount)
        {
            var tr = new Transaction() { Name = name, DebitAccount = dAccount, CreditAccount = cAccount, Date = date, Amount = amount};
            Context.Transactions.Add(tr);
            return tr;
        }
        private void CreateFixture1(AccountingDbContext context)
        {
            var faf = AddNewAccount("Furniture and fittings", AccountType.Asset);
            var stk = AddNewAccount("Stock", AccountType.Asset);
            var dbt = AddNewAccount("Debtors", AccountType.Asset);
            var bnk = AddNewAccount("Bank", AccountType.Asset);
            var csh = AddNewAccount("Cash", AccountType.Asset);
            var crd = AddNewAccount("Creditors", AccountType.Liability);
            var pft = AddNewAccount("Profit", AccountType.Capital);
            Context.SaveChanges();
            AddBalancetoAccount(faf, 500m);
            AddBalancetoAccount(stk, 310m);
            AddBalancetoAccount(dbt, 680m);
            AddBalancetoAccount(bnk, 1510m);
            AddBalancetoAccount(csh, 10m);
            AddBalancetoAccount(crd, 910m);
            Context.SaveChanges();
            AddNewTransaction("Sale to customer", stk, csh, new DateTime(2017, 5, 17), 10m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2017, 6, 5));
        }
        private void CreateFixture2(AccountingDbContext context)
        {
            var sog = AddNewAccount("Stock of goods", AccountType.Asset);
            var bld = AddNewAccount("Building", AccountType.Asset);
            var cab = AddNewAccount("Cash at Bank", AccountType.Asset);
            var dbt = AddNewAccount("Debtor", AccountType.Asset);
            var crd = AddNewAccount("Creditor", AccountType.Liability);
            var sha = AddNewAccount("Shareholders Account", AccountType.Capital);
            var pft = AddNewAccount("Profit", AccountType.Capital);
            Context.SaveChanges();
            AddNewTransaction("Sale to customer", sog, cab, new DateTime(2017, 5, 17), 108.45m);
            AddNewTransaction("Sale to customer", sog, cab, new DateTime(2017, 5, 25), 34.99m);
            AddNewTransaction("Stock refill", crd, sog, new DateTime(2017, 5, 18), 56.00m);
            AddNewTransaction("Purchase of stock", crd, sog, new DateTime(2017, 4, 01), 210.00m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2017, 6, 5));
            AddNewBalanceSheet(new DateTime(2017, 5, 17));
            AddNewBalanceSheet(new DateTime(2017, 5, 9));
        }
    }
}