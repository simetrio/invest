namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Интерфейс калькулятора транзакций
    /// </summary>
    public interface ITransactionCalculator
    {
        bool CanCalculate(OperationType operationType);

        ITransaction Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider
        );
    }
}