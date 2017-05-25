using NakedObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Accounting.Model
{
    public class Account
    {
        #region Injected Services
        //An implementation of this interface is injected automatically by the framework
        public IDomainObjectContainer Container { set; protected get; }
        #endregion
        [NakedObjectsIgnore]
        public virtual int Id { get; set; }
        [Title]//This property will be used for the object's title at the top of the view and in a link
        public virtual string AccountName { get; set; }

        #region DebitTransactions (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(Transaction.Name), nameof(Transaction.Date), nameof(Transaction.Amount), nameof(Transaction.CreditAccount))]
        public virtual ICollection<Transaction> DebitTransactions
        {
            get
            {
                var Id = this.Id;
                return Container.Instances<Transaction>().Where(t => t.DebitAccount.Id == Id).ToList();
            }
        }
        #endregion
       
        #region CreditTransactions (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(Transaction.Name), nameof(Transaction.Date), nameof(Transaction.Amount), nameof(Transaction.DebitAccount))]
        public virtual ICollection<Transaction> CreditTransactions
        {
            get
            {
                var Id = this.Id;
                return Container.Instances<Transaction>().Where(t => t.CreditAccount.Id == Id).ToList();
            }
        }
        #endregion

        public virtual decimal balance
        {
            get
            {
               decimal TotalCredit = CreditTransactions.Sum(c => c.Amount);
               decimal TotalDebit = DebitTransactions.Sum(d => d.Amount);
               decimal TotalBalance = TotalCredit - TotalDebit;
               return TotalBalance;
            }
        }
    }
}
