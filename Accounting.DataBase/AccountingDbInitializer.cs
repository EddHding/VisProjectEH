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
            var sog = AddNewAccount("Stock of goods");
            var bld = AddNewAccount("Building");
            var cab = AddNewAccount("Cash at Bank");
            Context.SaveChanges();
            AddNewTransaction("Sale to customer", sog, cab, new DateTime(2017, 5, 25), 108.45m);

        }

        private Account AddNewAccount(string name)
        {
            var ac = new Account() { AccountName = name };
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