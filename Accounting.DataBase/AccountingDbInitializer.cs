using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Accounting.Model;
using System;

namespace Accounting.DataBase
{
    public class AccountingDbInitializer : DropCreateDatabaseAlways<AccountingDbContext>
    {
        private Account stk;
        private Account bnk;
        private Account crd;
        private Account bld;
        private Account dbt;
        private Account sha;
        private Account pft;
        private Account csh;
        private AccountingDbContext Context;
        protected override void Seed(AccountingDbContext context)
        {
            this.Context = context;
            CreateStandardAccounts(context);
            Chapter3_RQ_3_5(context); //Chapter 3 review question 3.5
            //Chapter2_RQ_2_5A(context); //Chapter 2 review question 2.5A
            //Chapter1_RQ_1_14A(context); //Chapter 1 review question 1.14A
            //CreateFixture1(context); //Book example p.76 exhibit 8.2
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

        private void CreateFixture1(AccountingDbContext context)
        {
            var faf = AddNewAccount("Furniture and fittings", AccountType.Asset);
            Context.SaveChanges();
            AddNewTransaction("Initial Investment", bnk, sha, new DateTime(2017, 4, 2), 2100m);
            AddNewTransaction("Buy Stock", stk, bnk, new DateTime(2017, 4, 21), 300m);
            AddNewTransaction("Buy Furniture", faf, bnk, new DateTime(2017, 4, 23), 500m);
            AddNewTransaction("debtors", dbt, bnk, new DateTime(2017, 5, 16), 680m);
            AddNewTransaction("creditors", bnk, crd, new DateTime(2017, 5, 16), 910m);
            AddNewTransaction("Cash Withdrawl", csh, bnk, new DateTime(2017, 5, 16), 20m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2017, 4, 25));
            AddNewBalanceSheet(new DateTime(2017, 6, 5));
        }
        private void CreateFixture2(AccountingDbContext context)
        {
            AddNewTransaction("Sale to customer", stk, bnk, new DateTime(2017, 5, 17), 108.45m);
            AddNewTransaction("Sale to customer", stk, bnk, new DateTime(2017, 5, 25), 34.99m);
            AddNewTransaction("Stock refill", crd, stk, new DateTime(2017, 5, 18), 56.00m);
            AddNewTransaction("Purchase of stock", crd, stk, new DateTime(2017, 4, 01), 210.00m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2017, 6, 5));
            AddNewBalanceSheet(new DateTime(2017, 5, 17));
            AddNewBalanceSheet(new DateTime(2017, 5, 9));
        }
        private void Chapter1_RQ_1_14A(AccountingDbContext context)
        {
            var eqp = AddNewAccount("Equipment", AccountType.Asset);
            var veh = AddNewAccount("Vehicles", AccountType.Asset);
            Context.SaveChanges();
            //set up of initial values
            AddNewTransaction("Initial Investment", bnk, sha, new DateTime(2019, 11, 2), 33080m);
            AddNewTransaction("Buy Stock", stk, bnk, new DateTime(2019, 11, 2), 6150m);
            AddNewTransaction("Buy Equipment", eqp, bnk, new DateTime(2019, 11, 2), 11500m);
            AddNewTransaction("Buy Motor Vehicle", veh, bnk, new DateTime(2019, 11, 2), 6290m);
            AddNewTransaction("debtors", dbt, bnk, new DateTime(2019, 11, 2), 5770m);
            AddNewTransaction("creditors", bnk, crd, new DateTime(2019, 11, 2), 3950m);
            AddNewTransaction("Cash Withdrawl", csh, bnk, new DateTime(2019, 11, 2), 40m);
            //Transactions from the question
            AddNewTransaction("Extra Equipment", eqp, crd, new DateTime(2019, 12, 1), 1380m);
            AddNewTransaction("Buy Stock", stk, bnk, new DateTime(2019, 12, 2), 570m);
            AddNewTransaction("Pay Creditors", crd, bnk, new DateTime(2019, 12, 3), 790m);
            AddNewTransaction("Payed By Debtors", bnk, dbt, new DateTime(2019, 12, 4), 840m);
            AddNewTransaction("Payed By Debtors", csh, dbt, new DateTime(2019, 12, 5), 60m);
            AddNewTransaction("Dale Payment In", bnk, sha, new DateTime(2019, 12, 6), 250m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2019, 11, 3));
            AddNewBalanceSheet(new DateTime(2019, 12, 7));
        }
        private void Chapter2_RQ_2_5A(AccountingDbContext context)
        {
            var fix = AddNewAccount("Office Fixtures", AccountType.Asset);
            var veh = AddNewAccount("Vehicles", AccountType.Asset);
            Context.SaveChanges();
            AddNewTransaction("Initial Investment", bnk, sha, new DateTime(2019, 6, 1), 5000m);
            AddNewTransaction("Bought Van", veh, bnk, new DateTime(2019, 6, 2), 1200m);
            AddNewTransaction("Bought Office Fixtures from Young Ltd.", fix, crd, new DateTime(2019, 6, 5), 400m);
            AddNewTransaction("Bought Van from Super Motors", veh, crd, new DateTime(2019, 6, 8), 800m);
            AddNewTransaction("Cash Withdrawl", csh, bnk, new DateTime(2019, 6, 12), 100m);
            AddNewTransaction("Bought Office Fixtures", fix, csh, new DateTime(2019, 6, 15), 60m);
            AddNewTransaction("Payment to Super Motors", crd, bnk, new DateTime(2019, 6, 19), 800m);
            AddNewTransaction("Loan from J Jarvis", csh, crd, new DateTime(2019, 6, 21), 1000m);
            AddNewTransaction("Cash Deposit", bnk, csh, new DateTime(2019, 6, 25), 800m);
            AddNewTransaction("Bought Office Fixtures", fix, bnk, new DateTime(2019, 6, 30), 300m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2019, 6, 1));
            AddNewBalanceSheet(new DateTime(2019, 7, 2));
        }

        private void Chapter3_RQ_3_5(AccountingDbContext context)
        {
            //for additional accounts
            var fur = AddNewAccount("Office Furniture", AccountType.Asset);
            var veh = AddNewAccount("Vehicles", AccountType.Asset);
            Context.SaveChanges();
            //for transactions
            AddNewTransaction("Initial Investment", bnk, sha, new DateTime(2017, 7, 1), 10000m);
            AddNewTransaction("Loan from T Cooper", csh, crd, new DateTime(2017, 7, 2), 400m);
            AddNewTransaction("Bought stock from F Jones", stk, crd, new DateTime(2017, 7, 3), 840m);
            AddNewTransaction("Bought stock from S Charles", stk, crd, new DateTime(2017, 7, 3), 3600m);
            AddNewTransaction("Sold Stock", csh, stk, new DateTime(2017, 7, 4), 200m);
            AddNewTransaction("Cash Deposit", bnk, csh, new DateTime(2017, 7, 6), 250m);
            AddNewTransaction("Sold stock to C Moody", dbt, stk, new DateTime(2017, 7, 8), 180m);
            AddNewTransaction("Sold stock to J Newman", dbt, stk, new DateTime(2017, 7, 10), 220m);
            AddNewTransaction("Bought stock from F Jones", stk, crd, new DateTime(2017, 7, 11), 370m);
            AddNewTransaction("C Moody returned stock", stk, dbt, new DateTime(2017, 7, 12), 40m);
            AddNewTransaction("Sold stock to H Morgan", dbt, stk, new DateTime(2017, 7, 14), 190m);
            AddNewTransaction("Sold stock to J Peat", dbt, stk, new DateTime(2017, 7, 14), 320m);
            AddNewTransaction("Returned stock to F Jones", crd, stk, new DateTime(2017, 7, 15), 140m);
            AddNewTransaction("Bought van from Manchester Motors", veh, crd, new DateTime(2017, 7, 17), 2600m);
            AddNewTransaction("Bought office furniture from Faster Supplies Ltd", fur, crd, new DateTime(2017, 7, 18), 600m);
            AddNewTransaction("Returned stock to S Charles", crd, stk, new DateTime(2017, 7, 19), 110m);
            AddNewTransaction("Bought stock", stk, csh, new DateTime(2017, 7, 20), 220m);
            AddNewTransaction("Sold Stock", csh, stk, new DateTime(2017, 7, 24), 70m);
            AddNewTransaction("Paid Money owed to F Jones", crd, bnk, new DateTime(2017, 7, 25), 1070m);
            AddNewTransaction("H Morgan returned stock", stk, dbt, new DateTime(2017, 7, 26), 30m);
            AddNewTransaction("Returned Office furniture to Faster Supplies Ltd", crd, fur, new DateTime(2017, 7, 27), 160m);
            AddNewTransaction("Loan from E Sangster", csh, crd, new DateTime(2017, 7, 28), 500m);
            AddNewTransaction("Paid Money owed to Manchester Motors", crd, bnk, new DateTime(2017, 7, 29), 2600m);
            AddNewTransaction("Bought Office Furniture", fur, csh, new DateTime(2017, 7, 31), 100m);
            Context.SaveChanges();
            AddNewBalanceSheet(new DateTime(2017, 6, 30));
            AddNewBalanceSheet(new DateTime(2017, 8, 1));
        }
        private void CreateStandardAccounts(AccountingDbContext context)
        {
            stk = AddNewAccount("Stock", AccountType.Asset);
            bld = AddNewAccount("Building", AccountType.Asset);
            bnk = AddNewAccount("Bank", AccountType.Asset);
            dbt = AddNewAccount("Debtors", AccountType.Asset);
            crd = AddNewAccount("Creditors", AccountType.Liability);
            sha = AddNewAccount("Share Capital", AccountType.Capital);
            pft = AddNewAccount("Profit", AccountType.Capital);
            csh = AddNewAccount("Cash", AccountType.Asset);
            Context.SaveChanges();
        }
    }
}