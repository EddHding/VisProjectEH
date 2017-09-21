using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class BalanceSheet
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        public virtual DateTime Date { get; set; }

        public string Title()
        {
            var t = Container.NewTitleBuilder();
            t.Append(Date, "d", null);
            return t.ToString();
        }

        #region AccountBalances (collection)
        private ICollection<Balance> _AccountBalances = new List<Balance>(); 
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [NotPersisted, NotMapped, TableView(false, nameof(Balance.TypeOfAccount), nameof(Balance.AccountName), nameof(Balance.Amount))]
        public virtual ICollection<Balance> AccountBalances
        {
            get
            {
                var balances = new List<Balance>();
                Account[] ac = Container.Instances<Account>().ToArray();
                for (int i = 0; i < ac.Count(); i++) //for each is neater
                {
                    var ab = Container.NewViewModel<Balance>();
                    ab.AccountName = ac[i].AccountName;
                    ab.Amount = ac[i].balanceAtDate(Date); //will need to get balance for a certain date
                    ab.TypeOfAccount = ac[i].TypeOfAccount;
                    balances.Add(ab);
                }
                decimal totalassets = GetTotal(balances, AccountType.Asset);
                balances.Add(AccountingService.CreateNewBalance("Asset_Total", totalassets, AccountType.Total));

                decimal totalliabilities = GetTotal(balances, AccountType.Liability);
                balances.Add(AccountingService.CreateNewBalance("Liability_Total", totalliabilities, AccountType.Total));

                decimal totalAL = totalassets + totalliabilities;
                balances.Add(AccountingService.CreateNewBalance("Assets + Liabilities", totalAL, AccountType.Total));

                decimal totalcapital = GetTotal(balances, AccountType.Capital);
                balances.Add(AccountingService.CreateNewBalance("Capital_Total", totalcapital, AccountType.Total));

                return balances;
            }
        }

        [NakedObjectsIgnore]
        private static decimal GetTotal(List<Balance> balances, AccountType type)
        {
            return balances.Where(a => a.TypeOfAccount == type).Sum(a => a.Amount);
        }
        #endregion

        public virtual decimal Total
        {
            get
            {
                decimal totalassets = AccountBalances.Where(a => a.TypeOfAccount == AccountType.Asset).Sum(a => a.Amount);
                decimal totalliabilities = AccountBalances.Where(a => a.TypeOfAccount == AccountType.Liability).Sum(a => a.Amount);
                decimal totalAL = totalassets + totalliabilities;
                decimal totalcapital = AccountBalances.Where(a => a.TypeOfAccount == AccountType.Capital).Sum(a => a.Amount);
                decimal total = totalAL + totalcapital;
                return total;
            }
        }

    }
}
