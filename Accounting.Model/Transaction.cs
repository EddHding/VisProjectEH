using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class Transaction
    {
        [NakedObjectsIgnore]
        public virtual int ID { get; set; }
        public virtual Account DebitAccount { get; set; }
        public virtual Account CreditAccount { get; set; }
        public virtual DateTime Date { get; set;}
        public virtual Decimal Amount { get; set; }
    }
}
