using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class AccountBalance
    {
        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [Title]
        public virtual String AccountName { get; set; }


        public virtual Decimal Balance { get; set; }

        public AccountBalance(Account account)
        {
            AccountName = account.AccountName;
            Balance = account.balance;
        }
    }
}
