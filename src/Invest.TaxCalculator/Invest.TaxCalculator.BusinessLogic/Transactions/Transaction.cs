using System.Linq;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class Transaction
    {
        public string Ticker { get; set; }

        public TransactionType Type { get; set; }

        public TransactionOperation[] Operations { get; set; }

        public static Transaction Create(string ticker, TransactionType type, TransactionOperation[] operations)
        {
            return new Transaction
            {
                Ticker = ticker,
                Type = type,
                Operations = operations,
            };
        }
    }
}