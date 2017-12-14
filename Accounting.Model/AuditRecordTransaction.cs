using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class AuditRecordTransaction : AuditRecord
    {

        [NakedObjectsIgnore]
        public virtual int TransactionID { get; set; }

        [NotMapped]
        public virtual Transaction Transaction
        {
            get { return Container.Instances<Transaction>().Single(t => t.Id == TransactionID); }
        }


        //public virtual Transaction Transaction { get; set; }


    }
}
