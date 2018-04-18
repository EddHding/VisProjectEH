using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class Transaction : AbstractTransaction
    {
        [NakedObjectsIgnore]
        public virtual int? DebitAccountId { get; set; }

        [MemberOrder(4)]
        public virtual Account DebitAccount { get; set; }

        [PageSize(10)]
        public IQueryable<Account> AutoCompleteDebitAccount([MinLength(2)] string matching)
        {
            return AccountingService.FindAccountByName(matching);
        }

        [NakedObjectsIgnore]
        public virtual int? CreditAccountId { get; set; }

        [MemberOrder(5)]
        public virtual Account CreditAccount { get; set; }

        [PageSize(10)]
        public IQueryable<Account> AutoCompleteCreditAccount([MinLength(2)] string matching)
        {
            return AccountingService.FindAccountByName(matching);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
