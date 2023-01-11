using System.Collections.Generic;
using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Calculator
{
    /// <summary>
    ///     Калькулятор транзакции сделки по купле продаже облигации
    /// </summary>
    public class BuySellBondTransactionCalculator : ITransactionCalculator
    {
        public bool CanCalculate(OperationType operationType)
        {
            return operationType == OperationType.SellBond;
        }

        public IEnumerable<Transaction> Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider
        )
        {
            var sellCommission = childOperationsProvider.TryGet(operation.Id);
            var buys = buyOperationsIterator.Get(operation);

            foreach (var buy in buys)
            {
                var buyCommission = childOperationsProvider.TryGet(buy.Operation.Id);

                var buyTransactionOperation = TransactionOperation.Debit(buy.Operation, buy.Count);
                var buyCommissionTransactionOperation = buyCommission != null
                    ? TransactionOperation.Commission(buyCommission, buy.Count, buy.Operation.Count)
                    : null;
                
                var sellTransactionOperation = TransactionOperation.Credit(operation, buy.Count);
                var sellCommissionTransactionOperation = sellCommission != null
                    ? TransactionOperation.Commission(sellCommission, buy.Count, operation.Count)
                    : null;

                var transactionOperations = new[]
                    {
                        buyTransactionOperation,
                        buyCommissionTransactionOperation!,
                        sellTransactionOperation,
                        sellCommissionTransactionOperation!,
                    }
                    .Where(x => x != null)
                    .ToArray();

                yield return Transaction.Create(operation.Ticker, TransactionType.SellBond, transactionOperations);
            }
        }
    }
}