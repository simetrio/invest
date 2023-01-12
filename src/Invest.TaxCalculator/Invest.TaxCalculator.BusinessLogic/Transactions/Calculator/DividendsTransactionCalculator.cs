using System.Collections.Generic;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Calculator
{
    /// <summary>
    ///     Калькулятор транзакции дивидендов
    /// </summary>
    public class DividendsTransactionCalculator : ITransactionCalculator
    {
        public bool CanCalculate(OperationType operationType)
        {
            return operationType == OperationType.Dividends;
        }

        public IEnumerable<Transaction> Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider
        )
        {
            var dividendsTransactionOperation = TransactionOperation.Credit(operation, operation.Count);

            yield return Transaction.Create(operation, TransactionType.Dividends, new[] {dividendsTransactionOperation});
        }
    }
}