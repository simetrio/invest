using Invest.TaxCalculator.BusinessLogic.Countries;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Dto
{
    public class TransactionDto
    {
        public string Ticker { get; set; }

        public Country Country { get; set; }

        public TransactionType Type { get; set; }

        public DateTime DateTime { get; set; }

        public TransactionOperationDto[] Operations { get; set; }
    }
}