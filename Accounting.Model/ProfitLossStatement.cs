using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class ProfitLossStatement
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(StartDate, "d", null).Append(" to ").Append(EndDate, "d", null);
            return t.ToString();
        }

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [MemberOrder(1)]
        public virtual DateTime StartDate { get; set; }

        [MemberOrder(2)]
        public virtual DateTime EndDate { get; set; }

        public string ValidateEndDate(DateTime endDate)
        {
            var rb = new ReasonBuilder();
            rb.AppendOnCondition(endDate > DateTime.Today, "Date can't be in the future!");
            return rb.Reason;
        }

        public string Validate(DateTime StartDate, DateTime EndDate)
        {
            var rb = new ReasonBuilder();
            rb.AppendOnCondition(StartDate > EndDate, "Start Date must be before End Date!");
            rb.AppendOnCondition(StartDate > DateTime.Today, "Date can't be in the future!");
            return rb.Reason;
        }

        #region SaleTransactions (collection)
        [NotPersisted, NotMapped, TableView(false, nameof(SaleTransaction.Name), nameof(SaleTransaction.Date), nameof(SaleTransaction.Amount), nameof(SaleTransaction.StockPrice), nameof(SaleTransaction.DepositAccount))]
        public virtual ICollection<SaleTransaction> SaleTransactions
        {
            get
            {
                var ID = this.Id;
                return Container.Instances<SaleTransaction>().Where(t => t.Date > StartDate && t.Date < EndDate).ToList();
            }
        }
        #endregion

        public virtual Decimal Profit
        {
            get
            {
                return SaleTransactions.Sum(s => s.profit);
            }
        }

    }
}
