namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Коллекция транзакций
    /// </summary>
    public class TransactionsCollection
    {
        public TransactionsCollection(Transaction[] transactions)
        {
            All = transactions;
        }

        public Transaction[] All { get; }
    }
}