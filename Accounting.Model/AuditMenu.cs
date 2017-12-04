using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class AuditMenu
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        public IQueryable<AuditRecord> ListAuditRecords()
        {
            return Container.Instances<AuditRecord>();
        }
    }
}
