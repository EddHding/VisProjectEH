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
            CreateFixture1(context); //Book example p.76 exhibit 8.2
            //CreateFixture2(context); //Original data
        }

        private void AddNewProfitLossStatement()
        {
            //var pls = new ProfitLossStatement {};
            //Context.ProfitLossStatements.Add(pls);
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

        private StartingTransaction AddNewStartingBalance(Account cAccount, DateTime date, decimal amount)
        {
            var tr = new StartingTransaction() {CreditAccount = cAccount, Date = date, Amount = amount };
            Context.Transactions.Add(tr);
            return tr;
        }

        private ProfitLossTransaction AddNewProfitLossTransaction(string name, Account dAccount, Account cAccount, DateTime date, decimal amount, decimal costofstock)
        {
            var pltr = new ProfitLossTransaction() { Name = name, DebitAccount = dAccount, CreditAccount = cAccount, Date = date, Amount = amount, CostofStock = costofstock };
            Context.Transactions.Add(pltr);
            return pltr;
        }

        private void CreateFixture1(AccountingDbContext context)
        {
            var faf = AddNewAccount("Furniture and fittings", AccountType.Asset);
            var stk = AddNewAccount("Stock", AccountType.Asset);
            var dbt = AddNewAccount("Debtors", AccountType.Asset);
            var bnk = AddNewAccount("Bank", AccountType.Asset);
            var csh = AddNewAccount("Cash", AccountType.Asset);
            var crd = AddNewAccount("Creditors", AccountType.Liability);
            var cap = AddNewAccount("Share Capital", AccountType.Capital);
            Context.SaveChanges();
            //AddNewStartingBalance(faf, new DateTime(2017, 5, 16), 500);
            //AddNewStartingBalance(stk, new DateTime(2017, 5, 16), 310);
            //AddNewStartingBalance(dbt, new DateTime(2017, 5, 16), 680);
            //AddNewStartingBalance(bnk, new DateTime(2017, 5, 16), 1510);
            //AddNewStartingBalance(csh, new DateTime(2017, 5, 16), 10);
            //AddNewStartingBalance(crd, new DateTime(2017, 5, 16), -910);
            Context.SaveChanges();
            AddNewTransaction("Initial Investment", bnk, cap, new DateTime(2017, 5, 16), 2100m);
            AddNewTransaction("Buy Stock", stk, bnk, new DateTime(2017, 5, 16), 300m);
            AddNewTransaction("Buy Furniture", faf, bnk, new DateTime(2017, 5, 16), 500m);
            AddNewTransaction("debtors", dbt, bnk, new DateTime(2017, 5, 16), 680m);
            AddNewTransaction("creditors", bnk, crd, new DateTime(2017, 5, 16), 910m);
            AddNewTransaction("Cash Withdrawl", csh, bnk, new DateTime(2017, 5, 16), 20m);
           // AddNewProfitLossTransaction("Sale to Customer", bnk, stk, new DateTime(2017, 5, 16), 100m, 20m);
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