using System.Collections.Generic;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Calculator
{
    /// <summary>
    ///     Интерфейс калькулятора транзакций
    /// </summary>
    public interface ITransactionCalculator
    {
        bool CanCalculate(OperationType operationType);

        IEnumerable<Transaction> Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider
        );
    }
}