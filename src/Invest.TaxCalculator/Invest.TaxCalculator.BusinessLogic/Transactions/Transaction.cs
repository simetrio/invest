namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class Transaction
    {
        public string Ticker { get; set; }
        
        public TransactionOperation[] Operations { get; set; }

        public static Transaction Create(string ticker, TransactionOperation[] operations)
        {
            return new Transaction
            {
                Ticker = ticker,
                Operations = operations,
            };
        }
    }
}