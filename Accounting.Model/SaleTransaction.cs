using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class SaleTransaction : AbstractTransaction
    {
        public virtual Decimal StockPrice { get; set; }

        [NakedObjectsIgnore]
        public virtual int? ProfitAccountId { get; set; }

        public virtual Account ProfitAccount { get; set; }

        public Account DefaultProfitAccount()
        {
            return AccountingService.FindAccountByName("Profit").ToArray()[0];
        }

        [NakedObjectsIgnore]
        public virtual int? StockAccountId { get; set; }

        public virtual Account StockAccount { get; set; }

        public Account DefaultStockAccount()
        {
            return AccountingService.FindAccountByName("Stock").ToArray()[0];
        }


        public virtual Account DepositAccount { get; set; } //cash or bank credit this

        public IList<Account> ChoicesDepositAccount()
        {
            List<Account> Choices = new List<Account>();
            Choices.Add(AccountingService.FindAccountByName("Bank").ToArray()[0]);
            Choices.Add(AccountingService.FindAccountByName("Cash").ToArray()[0]);
            return Choices;
        }

        public virtual Decimal profit
        {
            get
            {
                return Amount - StockPrice;
            }
        }
    }
}
