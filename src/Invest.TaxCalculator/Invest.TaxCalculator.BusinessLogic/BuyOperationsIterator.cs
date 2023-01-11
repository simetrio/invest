namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     По методу FIFO отдает покупки
    /// </summary>
    public class BuyOperationsIterator
    {
        private readonly OperationsCollection _operations;
        private readonly TransactionsCollection _transactions;

        public BuyOperationsIterator(OperationsCollection operations, TransactionsCollection transactions)
        {
            _operations = operations;
            _transactions = transactions;
        }
    }
}