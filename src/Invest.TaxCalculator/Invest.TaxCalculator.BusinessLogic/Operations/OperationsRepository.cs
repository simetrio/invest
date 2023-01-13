using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    public class OperationsRepository : EntityRepository<Operation>
    {
        protected override void CreateOrUpdate(StorageElement storageElement, Operation[] items)
        {
            storageElement.Operations = storageElement
                .Operations
                .Except(items)
                .Concat(items)
                .ToArray();
        }

        protected override Operation[] ReadAll(StorageElement storageElement)
        {
            return storageElement.Operations;
        }

        protected override void Delete(StorageElement storageElement, Operation[] items)
        {
            storageElement.Operations = storageElement
                .Operations
                .Except(items)
                .ToArray();
        }
    }
}