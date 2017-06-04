using NakedObjects;
using System.Linq;


namespace Accounting.Model
{
    //This example service acts as both a 'repository' (with methods for
    //retrieving objects from the database) and as a 'factory' i.e. providing
    //one or more methods for creating new object(s) from scratch.
    public class AccountingService
    {
        #region Injected Services
        //An implementation of this interface is injected automatically by the framework
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public IQueryable<Account> AllAccounts()
        {
            //The 'Container' masks all the complexities of 
            //dealing with the database directly.
            return Container.Instances<Account>();
        }

        public IQueryable<Transaction> AllTransactions()
        {
            return Container.Instances<Transaction>().OrderByDescending(t => t.Date);
        }

        public IQueryable<BalanceSheet> AllBalanceSheets()
        {
            return Container.Instances<BalanceSheet>();
        }

        public IQueryable<Account> FindAccountByName(string name)
        {
            //Filters students to find a match
            return AllAccounts().Where(c => c.AccountName.ToUpper().Contains(name.ToUpper()));
        }

        public Transaction CreateNewTransaction()
        {
            Transaction obj = Container.NewTransientInstance<Transaction>();
            //set up any parameters
            //Container.Persist(ref obj);
            return obj;
        }

        public BalanceSheet CreateNewBalanceSheet()
        {
            Account[] ac = AllAccounts().ToArray();
            new AccountBalance(ac[0]);
            new AccountBalance(ac[1]);
            new AccountBalance(ac[2]);
            BalanceSheet obj = Container.NewTransientInstance<BalanceSheet>();
            return obj;
        }


    }

}
