using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class ProfitLossTransaction : AbstractTransaction
    {
        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(Name).Append(Date, "d", null);
            return t.ToString();
        }

        [MemberOrder(1)]
        public virtual string Name { get; set; }

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
        public virtual Decimal CostofStock { get; set; }

        [MemberOrder(5)]
        public virtual Decimal Profit
        {
          get
          {
              Decimal Profit = Amount - CostofStock;
              return Profit;
          }
        }
    }
}
