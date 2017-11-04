using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class PLAccount
    {
        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        public virtual String Name { get; set; }

        public virtual Decimal Ammount { get; set; }

    }
}
