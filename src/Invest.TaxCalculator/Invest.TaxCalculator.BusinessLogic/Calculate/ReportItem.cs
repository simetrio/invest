using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Calculate
{
    public class ReportItem
    {
        public Country Country { get; set; }

        public TransactionType Type { get; set; }

        public decimal Profit { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal Tax { get; set; }

        public static ReportItem Create(
            Country country,
            TransactionType type,
            decimal profit,
            decimal taxPercent,
            decimal tax
        )
        {
            return new ReportItem
            {
                Country = country,
                Type = type,
                Profit = profit,
                TaxPercent = taxPercent,
                Tax = tax,
            };
        }
    }
}