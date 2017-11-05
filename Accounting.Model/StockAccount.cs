using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class StockAccount
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }
        #endregion
        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        public virtual string Name
        {
            get
            {
                string name = AccountingService.FindAccountByName("Stock").ElementAt(0).AccountName;
                return name;
            }
        }

        public virtual DateTime Date { get; set; }

        public virtual decimal Ammount
        {
            get
            {
                decimal value = AccountingService.FindAccountByName("Stock").ElementAt(0).balance;
                return value;
            }
        }

        public StockAccount(DateTime date)
        {
            Date = date;
        }
        StockAccount Stock = new StockAccount(DateTime.Now);
    }
}
