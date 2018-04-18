using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public abstract class AbstractTransaction
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(Name).Append(Date, "d", null);
            return t.ToString();
        }

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [MemberOrder(1)]
        public virtual string Name { get; set; }

        [MemberOrder(2)]
        public virtual DateTime Date { get; set; }

        public string ValidateDate(DateTime date)
        {
            var rb = new ReasonBuilder();
            rb.AppendOnCondition(date > DateTime.Today, "Date can't be in the future!");
            return rb.Reason;
        }


        [MemberOrder(3)]
        public virtual Decimal Amount { get; set; }

        public string ValidateAmount(Decimal amount)
        {
            var rb = new ReasonBuilder();
            rb.AppendOnCondition(amount < 1, "Amount must be greater than 0!");
            return rb.Reason;
        }

        public IQueryable<AuditRecordTransaction> ShowHistory()
        {
            int thisId = this.Id;
            return Container.Instances<AuditRecordTransaction>().Where(art => art.TransactionID == thisId);
        }
    }
}
