using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class Balances : IViewModel
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [Title]
        public virtual String AccountName { get; set; }

        public virtual Decimal Balance { get; set; }

        public virtual AccountType TypeOfAccount { get; set; }

        public string[] DeriveKeys()
        {
            return new string[] { AccountName, Balance.ToString() };
        }

        public void PopulateUsingKeys(string[] keys)
        {
            AccountName = keys[0];
            Balance = Convert.ToDecimal(keys[1]);
        }
    }
}
