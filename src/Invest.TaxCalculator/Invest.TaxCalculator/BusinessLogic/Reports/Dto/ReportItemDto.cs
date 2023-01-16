using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Reports.Dto
{
    public class ReportItemDto
    {
        public Country Country { get; set; }

        public TransactionType Type { get; set; }

        public decimal Profit { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal Tax { get; set; }
    }
}