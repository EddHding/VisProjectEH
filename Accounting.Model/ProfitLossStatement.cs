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

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; } //date restrictions for the given month 

        public virtual PLAccount PLAccount { get; set; }

        #region PLAccounts (collection)
        private ICollection<PLAccount> _PLAccounts = new List<PLAccount>();

        public virtual ICollection<PLAccount> PLAccounts
        {
            get
            {
                return _PLAccounts;
            }
            set
            {
                _PLAccounts = value;
            }
        }
        #endregion

    }
}
