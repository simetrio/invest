using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Calculate.CalculateItem
{
    public class UsBondCancellationItemCalculator : IItemCalculator
    {
        public bool CanCalculate(Country country, TransactionType type)
        {
            return country == Country.Us && type == TransactionType.BondCancellation;
        }

        public ReportItem Calculate(Transaction[] transactions)
        {
            return ReportItem.Create(Country.Us, TransactionType.BondCancellation, 0, 0, 0);
        }
    }
}