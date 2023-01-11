using System.Linq;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class Transaction
    {
        public string Ticker { get; set; }

        public TransactionType Type { get; set; }

        public TransactionOperation[] Operations { get; set; }

        public decimal Profit { get; set; }

        public static Transaction Create(string ticker, TransactionType type, TransactionOperation[] operations)
        {
            return new Transaction
            {
                Ticker = ticker,
                Type = type,
                Operations = operations,
                Profit = CalculateProfit(operations),
            };
        }

        private static decimal CalculateProfit(TransactionOperation[] operations)
        {
            return Calculate(TransactionOperationType.Credit) - Calculate(TransactionOperationType.Debit);

            decimal Calculate(TransactionOperationType type)
            {
                return operations
                    .Where(x => x.Type == type)
                    .Sum(x => x.Price * x.Count * x.DollarPrice);
            }
        }
    }
}