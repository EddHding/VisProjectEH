using System.Data.Entity;
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
    }

}
