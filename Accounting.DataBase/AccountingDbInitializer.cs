using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Accounting.Model;
using System;

namespace Accounting.DataBase
{
    public class AccountingDbInitializer : DropCreateDatabaseIfModelChanges<AccountingDbContext>
    {
        private AccountingDbContext Context;
        protected override void Seed(AccountingDbContext context)
        {
            this.Context = context;
            AddNewAccount("Stock of goods");
            AddNewAccount("Building");
            AddNewAccount("Cash at Bank");
            Context.SaveChanges();
            AddNewTransaction("Sale to customer", 1, 3, new DateTime(2017, 5, 25), 108.45m);

        }

        private void AddNewAccount(string name)
        {
            var ac = new Account() { AccountName = name };
            Context.Accounts.Add(ac);
        }

        private void AddNewTransaction(string name, int debitAccountId, int creditAccountId, DateTime date, decimal amount)
        {
            var tr = new Transaction() { Name = name, DebitAccountId = debitAccountId, CreditAccountId = creditAccountId, Date = date, Amount = amount};
            Context.Transactions.Add(tr);
        }
    }
}