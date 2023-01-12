using System.Collections.Generic;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Calculator
{
    /// <summary>
    ///     Калькулятор транзакции купона
    /// </summary>
    public class CouponsTransactionCalculator : ITransactionCalculator
    {
        public bool CanCalculate(OperationType operationType)
        {
            return operationType == OperationType.Coupons;
        }

        public IEnumerable<Transaction> Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider
        )
        {
            var couponTransactionOperation = TransactionOperation.Credit(operation, operation.Count);

            yield return Transaction.Create(operation, TransactionType.Coupons, new[] {couponTransactionOperation});
        }
    }
}