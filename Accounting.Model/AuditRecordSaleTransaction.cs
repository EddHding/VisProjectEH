using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class AuditRecordSaleTransaction:AuditRecord
    {
        [NakedObjectsIgnore]
        public virtual int SaleTransactionID { get; set; }

        [NotMapped]
        public virtual SaleTransaction SaleTransaction
        {
            get { return Container.Instances<SaleTransaction>().Single(t => t.Id == SaleTransactionID); }
        }
    }
}
