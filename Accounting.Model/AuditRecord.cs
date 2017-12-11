using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public abstract class AuditRecord
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        public override string ToString()
        {
            var t = Container.NewTitleBuilder();
            t.Append(ActionName).Append(Date, "d", null);
            return t.ToString();
        }

        public virtual string ActionName { get; set; }

        public virtual string UserName { get; set; }
        
        public virtual DateTime Date { get; set; }

    }
}
