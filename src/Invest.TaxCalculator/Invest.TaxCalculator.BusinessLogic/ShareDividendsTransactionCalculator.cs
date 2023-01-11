namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Калькулятор транзакции дивидендов
    /// </summary>
    public class ShareDividendsTransactionCalculator : ITransactionCalculator
    {
        public bool CanCalculate(OperationType operationType)
        {
            throw new System.NotImplementedException();
        }

        public ITransaction Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}