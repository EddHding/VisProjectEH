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
        public virtual ICollection<AbstractTransaction> CreditTransactions
        {
            get
            {
                var ID = this.Id;
                return Container.Instances<AbstractTransaction>().Where(t => t.CreditAccount.Id == ID).ToList();
            }
        }
        #endregion

        public virtual decimal balance
        {
            get
            {
               decimal TotalCredit = CreditTransactions.Sum(c => c.Amount);
               decimal TotalDebit = DebitTransactions.Sum(d => d.Amount);
               return TotalBalance(TotalCredit, TotalDebit);
            }
        }

        [NakedObjectsIgnore]
        public virtual decimal balanceAtDate(DateTime userDate)
        {
                decimal TotalCredit = CreditTransactions.Where(c => c.Date < userDate).Sum(c => c.Amount);
                decimal TotalDebit = DebitTransactions.Where(c => c.Date < userDate).Sum(d => d.Amount);
                return TotalBalance(TotalCredit, TotalDebit);
        }

        public virtual decimal TotalBalance( decimal tcredit, decimal tdebit)
        {
            decimal TotalBalance = tcredit - tdebit;
            return TotalBalance;
        }
    }
}
