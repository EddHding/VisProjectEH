using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Accounting.Model;

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
        }

        private void AddNewAccount(string name)
        {
            var ac = new Account() { AccountName = name };
            Context.Accounts.Add(ac);
        }
    }
}