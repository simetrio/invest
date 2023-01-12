using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Calculate.CalculateItem
{
    public class UsCouponsItemCalculator : ItemCalculatorBase
    {
        protected override decimal TaxPercent => 13m;
        
        protected override Country Country => Country.Us;
    
        protected override TransactionType Type => TransactionType.Coupons;
    }
}