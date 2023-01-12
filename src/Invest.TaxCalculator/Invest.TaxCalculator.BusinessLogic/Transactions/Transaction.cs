using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class Transaction
    {
        public string Ticker { get; set; }

        public Country Country { get; set; }

        public TransactionType Type { get; set; }

        public TransactionOperation[] Operations { get; set; }

        public static Transaction Create(
            Operation operation,
            TransactionType type,
            TransactionOperation[] operations
        )
        {
            return new Transaction
            {
                Ticker = operation.Ticker,
                Country = operation.Country,
                Type = type,
                Operations = operations,
            };
        }
    }
}