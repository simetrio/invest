using System.Collections.Generic;
using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Calculator
{
    /// <summary>
    ///     Калькулятор транзакции гашение облигации
    /// </summary>
    public class BondCancellationTransactionCalculator : ITransactionCalculator
    {
        public bool CanCalculate(OperationType operationType)
        {
            return operationType == OperationType.BondCancellation;
        }

        public IEnumerable<Transaction> Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider
        )
        {
            var buys = buyOperationsIterator.Get(operation);

            foreach (var buy in buys)
            {
                var buyCommission = childOperationsProvider.TryGet(buy.Operation.Id);

                var buyTransactionOperation = TransactionOperation.Create(buy.Operation, buy.Count);
                var buyCommissionTransactionOperation = buyCommission != null
                    ? TransactionOperation.CreateCommission(buyCommission, buy.Count)
                    : null;
                var cancellationTransactionOperation = TransactionOperation.Create(operation, buy.Count);

                var transactionOperations = new[]
                    {
                        buyTransactionOperation,
                        buyCommissionTransactionOperation!,
                        cancellationTransactionOperation
                    }
                    .Where(x => x != null)
                    .ToArray();

                yield return Transaction.Create(operation.Ticker, transactionOperations);
            }
        }
    }
}