using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Accounting.Model
{
    public class Account
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }
        [Title]//This property will be used for the object's title at the top of the view and in a link
        public virtual string AccountName { get; set; }

        public virtual AccountType TypeOfAccount { get; set; }

        internal static void Where(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        #region DebitTransactions (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(Transaction.Name), nameof(Transaction.Date), nameof(Transaction.Amount), nameof(Transaction.CreditAccount))]
        public virtual ICollection<Transaction> DebitTransactions
        {
            get
            {
                var ID = this.Id;
                return Container.Instances<Transaction>().Where(t => t.DebitAccount.Id == ID).ToList();
            }
        }
        #endregion
       
        #region CreditTransactions (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(Transaction.Name), nameof(Transaction.Date), nameof(Transaction.Amount), nameof(Transaction.DebitAccount))]
        public virtual ICollection<Transaction> CreditTransactions
        {
            get
            {
                var ID = this.Id;
                return Container.Instances<Transaction>().Where(t => t.CreditAccount.Id == ID).ToList();
            }
        }
        #endregion

        #region SaleTransactions (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(SaleTransaction.Name), nameof(SaleTransaction.Date), nameof(SaleTransaction.Amount), nameof(SaleTransaction.StockPrice), nameof(SaleTransaction.DepositAccount))]
        public virtual ICollection<SaleTransaction> SaleTransactions
        {
            get
            {
                var ID = this.Id;
                return Container.Instances<SaleTransaction>().Where(t => t.DepositAccount.Id == ID || t.ProfitAccount.Id == ID || t.StockAccount.Id == ID).ToList();
            }
        }
        #endregion

        public virtual decimal balance
        {
            get
            {
               decimal TotalCredit = CreditTransactions.Sum(c => c.Amount);
               decimal TotalDebit = DebitTransactions.Sum(d => d.Amount);
                decimal TotalSales;
                if (this.Id == AccountingService.FindAccountByName("Profit").ToArray()[0].Id)
                {
                    TotalSales = SaleTransactions.Sum(s => s.profit);
                    return TotalBalance(TotalCredit, TotalDebit) + TotalSales;
                }
                else if (this.Id == AccountingService.FindAccountByName("Stock").ToArray()[0].Id)
                {
                    TotalSales = SaleTransactions.Sum(s => s.StockPrice);
                    return TotalBalance(TotalCredit, TotalDebit) + TotalSales;
                }
                else if (this.Id == AccountingService.FindAccountByName("Bank").ToArray()[0].Id)
                {
                    TotalSales = SaleTransactions.Where(s => s.DepositAccount.Id == this.Id).Sum(s => s.Amount);
                    return TotalBalance(TotalCredit, TotalDebit) - TotalSales;
                }
                else if (this.Id == AccountingService.FindAccountByName("Cash").ToArray()[0].Id)
                {
                    TotalSales = SaleTransactions.Where(s => s.DepositAccount.Id == this.Id).Sum(s => s.Amount);
                    return TotalBalance(TotalCredit, TotalDebit) - TotalSales;
                }
                else
                {
                    return TotalBalance(TotalCredit, TotalDebit);
                }
            }
        }

        [NakedObjectsIgnore]
        public virtual decimal balanceAtDate(DateTime userDate)
        {
                decimal TotalCredit = CreditTransactions.Where(c => c.Date < userDate).Sum(c => c.Amount);
                decimal TotalDebit = DebitTransactions.Where(c => c.Date < userDate).Sum(d => d.Amount);
            decimal TotalSales;
            if (this.Id == AccountingService.FindAccountByName("Profit").ToArray()[0].Id)
            {
                TotalSales = SaleTransactions.Where(c => c.Date < userDate).Sum(s => s.profit);
                return TotalBalance(TotalCredit, TotalDebit) + TotalSales;
            }
            else if (this.Id == AccountingService.FindAccountByName("Stock").ToArray()[0].Id)
            {
                TotalSales = SaleTransactions.Where(c => c.Date < userDate).Sum(s => s.StockPrice);
                return TotalBalance(TotalCredit, TotalDebit) + TotalSales;
            }
            else if (this.Id == AccountingService.FindAccountByName("Bank").ToArray()[0].Id)
            {
                TotalSales = SaleTransactions.Where(s => s.DepositAccount.Id == this.Id).Where(c => c.Date < userDate).Sum(s => s.Amount);
                return TotalBalance(TotalCredit, TotalDebit) - TotalSales;
            }
            else if (this.Id == AccountingService.FindAccountByName("Cash").ToArray()[0].Id)
            {
                TotalSales = SaleTransactions.Where(s => s.DepositAccount.Id == this.Id).Where(c => c.Date < userDate).Sum(s => s.Amount);
                return TotalBalance(TotalCredit, TotalDebit) - TotalSales;
            }
            else
            {
                return TotalBalance(TotalCredit, TotalDebit);
            }
        }

        public virtual decimal TotalBalance( decimal tcredit, decimal tdebit)
        {
            decimal TotalBalance = tcredit - tdebit;
            return TotalBalance;
        }
    }
}
