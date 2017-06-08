﻿using NakedObjects;
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
        [NotPersisted, NotMapped, TableView(false, nameof(Balances.AccountName), nameof(Balances.Balance))]
        public virtual ICollection<Balances> AccountBalances
        {
            get
            {
                var balances = new List<Balances>();
                Account[] ac = Container.Instances<Account>().ToArray();
                for (int i = 0; i < 3; i++) //for each is neater
                {
                    var ab = Container.NewViewModel<Balances>();
                    ab.AccountName = ac[i].AccountName;
                    ab.Balance = ac[i].balanceAtDate(Date); //will need to get balance for a certain date
                    balances.Add(ab);
                }
                return balances;
            }
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
