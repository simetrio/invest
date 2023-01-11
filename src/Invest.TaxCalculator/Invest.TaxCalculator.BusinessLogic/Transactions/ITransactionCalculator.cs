using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
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