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
            if (queryOnly != true)
            {
                AuditRecordServices ar = Container.NewTransientInstance<AuditRecordServices>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.ActionName = actionName;
                ar.ServiceName = serviceName;
                ar.Date = DateTime.Now;
                Container.Persist(ref ar);
            }                            
        }

        public void ActionInvoked(IPrincipal byPrincipal, string actionName, object onObject, bool queryOnly, object[] withParameters)
        {
            if (queryOnly != true)
            {
                AuditRecordObjects ar = Container.NewTransientInstance<AuditRecordObjects>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.ActionName = actionName;
                ar.Object = onObject;
                ar.Date = DateTime.Now;
                Container.Persist(ref ar);
            }
        }

        public void ObjectPersisted(IPrincipal byPrincipal, object updatedObject)
        {
            // is assignable from determines whether it is a sub class 
            if (typeof(AuditRecord).IsAssignableFrom(updatedObject.GetType()) != true)
            {
                AuditRecordObjects ar = Container.NewTransientInstance<AuditRecordObjects>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.ActionName = ("Persisted " + updatedObject.ToString());
                ar.Object = updatedObject;
                ar.Date = DateTime.Now;
                Container.Persist(ref ar);
            }
        }

        public void ObjectUpdated(IPrincipal byPrincipal, object updatedObject)
        {
            AuditRecordObjects ar = Container.NewTransientInstance<AuditRecordObjects>();
            ar.UserName = byPrincipal.Identity.Name;
            ar.ActionName = ("Updated " + updatedObject.ToString());
            ar.Object = updatedObject;
            ar.Date = DateTime.Now;
            Container.Persist(ref ar);
        }
    }
}
