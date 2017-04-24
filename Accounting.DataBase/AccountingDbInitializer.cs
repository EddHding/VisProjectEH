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
            AddNewStudent("Alie Algol");
            AddNewStudent("Forrest Fortran");
            AddNewStudent("James Java");
        }

        private void AddNewStudent(string name)
        {
            var st = new Stock() { ItemName = name };
            Context.Students.Add(st);
        }
    }
}
