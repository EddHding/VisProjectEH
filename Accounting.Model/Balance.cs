using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class Balance : IViewModel
    {
        #region Injected Services
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [Title]
        public virtual String AccountName { get; set; }

        public virtual Decimal Amount { get; set; }

        public virtual AccountType TypeOfAccount { get; set; }

        public string[] DeriveKeys()
        {
            return new string[] { AccountName, Amount.ToString(), ((int)TypeOfAccount).ToString() };
        }

        public void PopulateUsingKeys(string[] keys)
        {
            AccountName = keys[0];
            Amount = Convert.ToDecimal(keys[1]);
            TypeOfAccount = (AccountType)Enum.Parse(typeof(AccountType), keys[2]); //how to turn an int into an enum.
        }
    }
}
