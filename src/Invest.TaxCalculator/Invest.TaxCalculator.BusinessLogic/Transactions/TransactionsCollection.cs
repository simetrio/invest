namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Коллекция транзакций
    /// </summary>
    public class TransactionsCollection
    {
        public TransactionsCollection(ITransaction[] transactions)
        {
            All = transactions;
        }

        public ITransaction[] All { get; }
    }
}