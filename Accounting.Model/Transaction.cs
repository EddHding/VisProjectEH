using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class Transaction
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }

        #endregion

        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(Name).Append(Date);
            return t.ToString();
        }

        [NakedObjectsIgnore]
        public virtual int ID { get; set; }

        [MemberOrder (1)]
        public virtual string Name { get; set; }

        [MemberOrder(4)]
        public virtual Account DebitAccount { get; set; }
        [PageSize(10)]
        public IQueryable<Account> AutoCompleteDebitAccount([MinLength(2)] string matching)
        {
            return AccountingService.FindAccountByName(matching);
        }

        [MemberOrder(5)]
        public virtual Account CreditAccount { get; set; }
        [PageSize(10)]
        public IQueryable<Account> AutoCompleteCreditAccount([MinLength(2)] string matching)
        {
            return AccountingService.FindAccountByName(matching);
        }

        [MemberOrder(2)]
        public virtual DateTime Date { get; set;}
        [MemberOrder (3)]
        public virtual Decimal Amount { get; set; }

    }
}
