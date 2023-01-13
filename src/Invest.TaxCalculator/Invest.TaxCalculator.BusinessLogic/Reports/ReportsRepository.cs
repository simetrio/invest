using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class ReportsRepository : EntityRepository<Report>
    {
        protected override void CreateOrUpdate(StorageElement storageElement, Report[] items)
        {
            storageElement.Reports = storageElement
                .Reports
                .Except(items)
                .Concat(items)
                .ToArray();
        }

        protected override Report[] ReadAll(StorageElement storageElement)
        {
            return storageElement.Reports;
        }

        protected override void Delete(StorageElement storageElement, Report[] items)
        {
            storageElement.Reports = storageElement
                .Reports
                .Except(items)
                .ToArray();
        }
    }
}