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
        private ICollection<Balances> _AccountBalances = new List<Balances>(); 
        [Eagerly(EagerlyAttribute.Do.Rendering)]
        [NotPersisted, NotMapped, TableView(false, nameof(Balances.TypeOfAccount), nameof(Balances.AccountName), nameof(Balances.Balance))]
        public virtual ICollection<Balances> AccountBalances
        {
            get
            {
                var balances = new List<Balances>();
                Account[] ac = Container.Instances<Account>().ToArray();
                for (int i = 0; i < ac.Count(); i++) //for each is neater
                {
                    var ab = Container.NewViewModel<Balances>();
                    ab.AccountName = ac[i].AccountName;
                    ab.Balance = ac[i].balanceAtDate(Date); //will need to get balance for a certain date
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

        private static decimal GetTotal(List<Balances> balances, AccountType type)
        {
            return balances.Where(a => a.TypeOfAccount == type).Sum(a => a.Balance);
        }
        #endregion

        public virtual decimal Total
        {
            get
            {
             return AccountBalances.Sum(b => b.Balance);
            }
        }

    }
}
