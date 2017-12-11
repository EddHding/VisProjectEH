using NakedObjects;
using System;
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
        ProfitLossField PLStock;
        ProfitLossField PLPrice;

        public IQueryable<Account> AllAccounts()
        {
            //The 'Container' masks all the complexities of 
            //dealing with the database directly.
            return Container.Instances<Account>().OrderBy(t => t.TypeOfAccount);
        }

        public IQueryable<AbstractTransaction> AllTransactions()
        {
            return Container.Instances<AbstractTransaction>().OrderByDescending(t => t.Date);
        }

        public IQueryable<BalanceSheet> AllBalanceSheets()
        {
            return Container.Instances<BalanceSheet>().OrderByDescending(t => t.Date);
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
            BalanceSheet bs = Container.NewTransientInstance<BalanceSheet>();
            bs.Date = DateTime.Today;
            Container.Persist(ref bs);
            return bs;
        }

        [NakedObjectsIgnore]
        public Balance CreateNewBalance(String name, Decimal balance, AccountType atype)
        {
            var ac = Container.NewViewModel<Balance>();
            ac.AccountName = name;
            ac.Amount = balance;
            ac.TypeOfAccount = atype;
            return ac;
        }

        public IQueryable<ProfitLossStatement> AllProfitLossStatements()
        {
            return Container.Instances<ProfitLossStatement>().OrderByDescending(t => t.EndDate);
        }

        public IQueryable<Sale> AllSales()
        {
            return Container.Instances<Sale>().OrderByDescending(t => t.Date);
        }

        public Sale CreateNewSale()
        {
            Sale obj = Container.NewTransientInstance<Sale>();
            //set up any parameters
            //Container.Persist(ref obj);
            return obj;
        }

        public ProfitLossStatement CreateProfitLossStatement(DateTime startdate, DateTime enddate)
        {
            ProfitLossStatement pls = Container.NewTransientInstance<ProfitLossStatement>();
            pls.StartDate = startdate;
            pls.EndDate = enddate;
            Account Stock = FindAccountByName("Stock").First();
            PLStock = CreateProfitLossField("Stock", Stock.balanceAtDate(enddate));
            pls.Stock = PLStock;
            PLPrice = CreateProfitLossField("Sale Price", 0m);
            pls.TotalSales = PLPrice;
            Container.Persist(ref pls);
            return pls;
        }

        [NakedObjectsIgnore]
        public IQueryable<ProfitLossField> FindProfitLossFieldByName(string name)
        {
            //Filters students to find a match
            return Container.Instances<ProfitLossField>().Where(c => c.Name.ToUpper().Contains(name.ToUpper()));
        }

        [NakedObjectsIgnore]
        public ProfitLossField CreateProfitLossField(string name, decimal balance)
        {
            ProfitLossField obj = Container.NewTransientInstance<ProfitLossField>();
            obj.Name = name;
            obj.Balance = balance;
            return obj;
        }

        public void SetUp()
        {
            //additional
            //SalesAccount PLStock = AddNewSalesAccount("Stock", stk.balanceAtDate(new DateTime(2017, 7, 31)));
            //SalesAccount PLPrice = AddNewSalesAccount("Sale Price", 0m);
            //AddNewProfitLossStatement(new DateTime(2017, 7, 1), new DateTime(2017, 7, 31), PLStock, PLPrice);
            CreateProfitLossStatement(new DateTime(2017, 6, 30), new DateTime(2017, 8, 1));
        }

    }

}
