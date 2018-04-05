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

        public IQueryable<AuditRecord> ListUpdatedObjects()
        {
            return Container.Instances<AuditRecord>().Where(ar => ar.Type == AuditType.ObjectUpdated);
        }

        public IQueryable<AuditRecord> ListPersistedObjects()
        {
            return Container.Instances<AuditRecord>().Where(ar => ar.Type == AuditType.ObjectPersisted);
        }

        public IQueryable<AuditRecord> ListObjectActions()
        {
            return Container.Instances<AuditRecord>().Where(ar => ar.Type == AuditType.ObjectAction);
        }

        public IQueryable<AuditRecord> ListServiceActions()
        {
            return Container.Instances<AuditRecord>().Where(ar => ar.Type == AuditType.ServiceAction);
        }
    }
}
