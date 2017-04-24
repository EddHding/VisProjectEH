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

        public DbSet<Stock> Students { get; set; }
    }

}
