using NakedObjects;
using System.Linq;


namespace Accounting.Model
{
    //This example service acts as both a 'repository' (with methods for
    //retrieving objects from the database) and as a 'factory' i.e. providing
    //one or more methods for creating new object(s) from scratch.
    public class AccountingService
    {
        #region Injected Services
        //An implementation of this interface is injected automatically by the framework
        public IDomainObjectContainer Container { set; protected get; }
        #endregion
        public Stock CreateNewStockItem()
        {
            //'Transient' means 'unsaved' -  returned to the user
            //for fields to be filled-in and the object saved.
            return Container.NewTransientInstance<Stock>();
        }

        public IQueryable<Stock> AllStock()
        {
            //The 'Container' masks all the complexities of 
            //dealing with the database directly.
            return Container.Instances<Stock>();
        }

        public IQueryable<Stock> FindStockByName(string name)
        {
            //Filters students to find a match
            return AllStock().Where(c => c.ItemName.ToUpper().Contains(name.ToUpper()));
        }
    }

}
