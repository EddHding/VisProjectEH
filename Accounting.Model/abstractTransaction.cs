using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public abstract class AbstractTransaction
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }
        
        [MemberOrder(2)]
        public virtual DateTime Date { get; set; }

        [MemberOrder(3)]
        public virtual Decimal Amount { get; set; }

        [NakedObjectsIgnore]
        public virtual int? CreditAccountId { get; set; }

        [MemberOrder(5)]
        public virtual Account CreditAccount { get; set; }
        [PageSize(10)]
        public IQueryable<Account> AutoCompleteCreditAccount([MinLength(2)] string matching)
        {
            return AccountingService.FindAccountByName(matching);
        }
    }
}
