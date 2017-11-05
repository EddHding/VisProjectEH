using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [MemberOrder(6)]
        public virtual DateTime Date { get; set; }

        [MemberOrder(5)]
        public virtual Decimal ValueOfStocksSold { get; set; }

        [MemberOrder(4)]
        public virtual Decimal SalePrice { get; set; }

        [MemberOrder(1)]
        public virtual string Name { get; set; }

        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(Name).Append(Date, "d", null);
            return t.ToString();
        }
    }
}
