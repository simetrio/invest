using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public IEnumerable<ITransaction> Calculate(
            Operation operation,
            BuyOperationsIterator buyOperationsIterator,
            ChildOperationsProvider childOperationsProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}