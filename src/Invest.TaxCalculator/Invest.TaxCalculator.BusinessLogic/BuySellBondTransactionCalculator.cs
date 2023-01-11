namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Калькулятор транзакции сделки по купле продаже облигации
    /// </summary>
    public class BuySellBondTransactionCalculator : ITransactionCalculator
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