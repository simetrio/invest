namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Калькулятор транзакции купона
    /// </summary>
    public class BondCouponsTransactionCalculator : ITransactionCalculator
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