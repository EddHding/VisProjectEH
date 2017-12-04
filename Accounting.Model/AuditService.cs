using NakedObjects.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using NakedObjects;

namespace Accounting.Model
{
    public class AuditService : IAuditor
    {
        #region Injected Services
        //An implementation of this interface is injected automatically by the framework
        public IDomainObjectContainer Container { set; protected get; }
        #endregion
        public void ActionInvoked(IPrincipal byPrincipal, string actionName, string serviceName, bool queryOnly, object[] withParameters)
        {
    
                AuditRecord ar = Container.NewTransientInstance<AuditRecord>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.ActionName = actionName;
                ar.ServiceName = serviceName;
                ar.Date = DateTime.Now;
                Container.Persist(ref ar);
            
        }

        public void ActionInvoked(IPrincipal byPrincipal, string actionName, object onObject, bool queryOnly, object[] withParameters)
        {
            
        }

        public void ObjectPersisted(IPrincipal byPrincipal, object updatedObject)
        {
            
        }

        public void ObjectUpdated(IPrincipal byPrincipal, object updatedObject)
        {
            
        }
    }
}
