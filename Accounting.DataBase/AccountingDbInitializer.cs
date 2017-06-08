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
            var sog = AddNewAccount("Stock of goods", AccountType.Asset);
            var bld = AddNewAccount("Building", AccountType.Asset);
            var cab = AddNewAccount("Cash at Bank", AccountType.Asset);
            AddNewAccount("Debtor", AccountType.Asset);
            AddNewAccount("Creditor", AccountType.Liability);
            AddNewAccount("Shareholders Account", AccountType.Capital);
            AddNewAccount("Profit", AccountType.Capital);
            Context.SaveChanges();
            AddNewTransaction("Sale to customer", sog, cab, new DateTime(2017, 5, 17), 108.45m);
            AddNewTransaction("Sale to customer", sog, cab, new DateTime(2017, 5, 25), 34.99m);
            AddNewTransaction("Stock refill", cab, sog, new DateTime(2017, 5, 18), 56.00m);
            AddNewTransaction("Purchase of stock", cab, sog, new DateTime(2017, 4, 01), 210.00m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2017, 6, 5));
            AddNewBalanceSheet(new DateTime(2017, 5, 17));
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

        private Transaction AddNewTransaction(string name, Account dAccount, Account cAccount, DateTime date, decimal amount)
        {
            var tr = new Transaction() { Name = name, DebitAccount = dAccount, CreditAccount = cAccount, Date = date, Amount = amount};
            Context.Transactions.Add(tr);
            return tr;
        }
    }
}