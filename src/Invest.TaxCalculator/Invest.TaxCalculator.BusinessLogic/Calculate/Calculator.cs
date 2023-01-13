using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Calculate.CalculateItem;
using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Calculate
{
    public class Calculator
    {
        private readonly IItemCalculator[] _itemCalculators =
        {
            new UsBuySellShareOrBondItemCalculator(),
            new UsDividendsItemCalculator(),
            new UsBondCancellationItemCalculator(),
            new UsCouponsItemCalculator(),
        };

        public Report Calculate(Transaction[] transactions, int year)
        {
            var items = transactions
                .GroupBy(x => (x.Country, x.Type))
                .Select(x => CalculateItem(x.Key.Country, x.Key.Type, x.ToArray()))
                .ToArray();

            return new Report
            {
                Year = year,
                Items = items,
            };
        }

        private ReportItem CalculateItem(Country country, TransactionType type, Transaction[] transactions)
        {
            var itemCalculator = _itemCalculators.Single(x => x.CanCalculate(country, type));

            return itemCalculator.Calculate(transactions);
        }
    }
}