﻿using NakedObjects.Audit;
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
        public AccountingService AccountingService { set; protected get; }
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
                ar.Type = AuditType.ServiceAction;
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
                ar.Type = AuditType.ObjectAction;
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
                ar.ActionName = ("Persisted ");
                ar.Object = updatedObject;
                ar.Date = DateTime.Now;
                ar.Type = AuditType.ObjectPersisted;
                Container.Persist(ref ar);
            }
        }

        public void ObjectUpdated(IPrincipal byPrincipal, Object updatedObject)
        {
            if (typeof(AuditRecord).IsAssignableFrom(updatedObject.GetType()))
            {
            }
            else if (typeof(Transaction).IsAssignableFrom(updatedObject.GetType()))
            {
                AuditRecordTransaction ar = Container.NewTransientInstance<AuditRecordTransaction>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.TransactionID = ((Transaction)updatedObject).Id; 
                ar.ActionName = ("Updated Transaction: " + ((Transaction)updatedObject).Name);
                ar.Date = DateTime.Now;
                ar.Type = AuditType.ObjectUpdated;
                Container.Persist(ref ar);
            }
            else if (typeof(SaleTransaction).IsAssignableFrom(updatedObject.GetType()))
            {
                AuditRecordSaleTransaction ar = Container.NewTransientInstance<AuditRecordSaleTransaction>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.SaleTransactionID = ((SaleTransaction)updatedObject).Id;
                ar.ActionName = ("Updated Transaction: " + ((SaleTransaction)updatedObject).Name);
                ar.Date = DateTime.Now;
                ar.Type = AuditType.ObjectUpdated;
                Container.Persist(ref ar);
            }
            else if (typeof(Account).IsAssignableFrom(updatedObject.GetType()))
            {
                AuditRecordAccount ar = Container.NewTransientInstance<AuditRecordAccount>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.AccountID = ((Account)updatedObject).Id;
                ar.ActionName = ("Updated Account: " + ((Account)updatedObject).AccountName);
                ar.Date = DateTime.Now;
                ar.Type = AuditType.ObjectUpdated;
                Container.Persist(ref ar);
            }
            else
            {
                AuditRecordObjects ar = Container.NewTransientInstance<AuditRecordObjects>();
                ar.UserName = byPrincipal.Identity.Name;
                ar.Object = updatedObject.GetType().BaseType.Name;
                ar.ActionName = ("Updated Object");
                ar.Date = DateTime.Now;
                ar.Type = AuditType.ObjectUpdated;
                Container.Persist(ref ar);
            }
            
        }
    }
}
