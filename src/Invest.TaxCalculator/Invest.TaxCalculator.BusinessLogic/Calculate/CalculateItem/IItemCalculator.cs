using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Calculate.CalculateItem
{
    public interface IItemCalculator
    {
        bool CanCalculate(Country country, TransactionType type);

        ReportItem Calculate(Transaction[] transactions);
    }
}