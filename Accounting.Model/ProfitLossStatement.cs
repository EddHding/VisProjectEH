using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class ProfitLossStatement
    {
        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [MemberOrder(1)]
        public virtual DateTime StartDate { get; set; }

        [MemberOrder(2)]
        public virtual DateTime EndDate { get; set; } //date restrictions for the given month 

    }
}
