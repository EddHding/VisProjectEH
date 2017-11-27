using NakedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class ProfitLossStatement
    {
        #region Injected Services
        public AccountingService AccountingService { set; protected get; }

        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        [NakedObjectsIgnore]
        public virtual int Id { get; set; }

        [MemberOrder(1)]
        public virtual DateTime StartDate { get; set; }

        [MemberOrder(2)]
        public virtual DateTime EndDate { get; set; } //date restrictions for the given month

        [MemberOrder(3)]
        public virtual ProfitLossField Stock { get; set; }

        [MemberOrder(4)]
        public virtual ProfitLossField TotalSales { get; set; }




        public virtual decimal GrossProfit
        {
            get
            {
                var OGStock = Stock.Balance;
                var StockChange = Stock.Balance;
                var TSales = TotalSales.Balance;
                var Sales = Container.Instances<Sale>().ToList();
                foreach (var s in Sales)
                {
                    StockChange = Stock.Balance - s.ValueOfStocksSold;
                    TSales = TotalSales.Balance + s.SalePrice;
                }
                StockChange = OGStock - StockChange;
                var profit = TSales - StockChange;
                return profit;
            }
        }

    }
}
