using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class AuditRecordAccount : AuditRecord
    {
        [NakedObjectsIgnore]
        public virtual int AccountID { get; set; }

        [NotMapped]
        public virtual Account Account
        {
            get { return Container.Instances<Account>().Single(t => t.Id == AccountID); }
        }
    }
}
