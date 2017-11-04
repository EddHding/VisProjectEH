using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class Sale
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Decimal ValueOfStocksSold { get; set; }

        public virtual Decimal SalePrice { get; set; }

        public virtual string Name { get; set; }

        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(Name).Append(Date, "d", null);
            return t.ToString();
        }
    }
}
