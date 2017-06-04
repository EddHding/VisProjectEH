using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class BalanceSheet
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        public virtual DateTime Date { get; set; }

        #region AccountBalances (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(AccountBalance.AccountName), nameof(AccountBalance.Balance))]
        public virtual ICollection<AccountBalance> AccountBalances
        {
            get
            {
                return Container.Instances<AccountBalance>().ToList();
            }
        }
        #endregion

    }
}
