using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class TransactionsRepository : EntityRepository<Transactions>
    {
        protected override void CreateOrUpdate(StorageElement storageElement, Transactions[] items)
        {
            storageElement.Transactions = storageElement
                .Transactions
                .Except(items)
                .Concat(items)
                .ToArray();
        }

        protected override Transactions[] ReadAll(StorageElement storageElement)
        {
            return storageElement.Transactions;
        }

        protected override void Delete(StorageElement storageElement, Transactions[] items)
        {
            storageElement.Transactions = storageElement
                .Transactions
                .Except(items)
                .ToArray();
        }
    }
}