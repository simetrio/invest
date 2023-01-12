using System;
using System.Collections.Generic;
using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Providers;

namespace Invest.TaxCalculator.BusinessLogic.Transactions.Calculator
{
    /// <summary>
    ///     Калькулятор транзакций
    /// </summary>
    public class TransactionsCalculator
    {
        private readonly ITransactionCalculator[] _transactionCalculators =
        {
            new BuySellShareOrBondTransactionCalculator(),
            new DividendsTransactionCalculator(),
            new BondCancellationTransactionCalculator(),
            new CouponsTransactionCalculator(),
        };

        public IEnumerable<Transaction> Calculate(
            OperationsCollection operations,
            TransactionsCollection transactions,
            int year
        )
        {
            var buyOperationsIterator = new BuyOperationsIterator(operations, transactions);
            var childOperationsProvider = new ChildOperationsProvider(operations);

            var operationsToCalculate = operations
                .All
                .Where(x => NeedCalculate(x, year));

            foreach (var operation in operationsToCalculate)
            {
                var calculator = _transactionCalculators
                    .Single(x => x.CanCalculate(operation.Type));

                var calculatedTransactions = calculator
                    .Calculate(operation, buyOperationsIterator, childOperationsProvider);

                foreach (var transaction in calculatedTransactions)
                {
                    yield return transaction;
                }
            }
        }

        private bool NeedCalculate(Operation operation, int year)
        {
            if (operation.DateTime.Year != year)
            {
                return false;
            }

            return operation.Type switch
            {
                OperationType.SellShare
                    or OperationType.Dividends
                    or OperationType.SellBond
                    or OperationType.BondCancellation
                    or OperationType.Coupons => true,

                OperationType.BuyShare
                    or OperationType.BuyBond
                    or OperationType.Comission => false,

                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}