using System;
using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Reports;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Calculate.CalculateItem
{
    public abstract class ItemCalculatorBase : IItemCalculator
    {
        protected abstract decimal TaxPercent { get; }

        protected abstract Country Country { get; }

        protected abstract TransactionType Type { get; }

        public bool CanCalculate(Country country, TransactionType type)
        {
            return country == Country && type == Type;
        }

        public ReportItem Calculate(Transaction[] transactions)
        {
            var profit = CalculateProfit(transactions);
            var tax = CalculateTax(profit, TaxPercent);

            return ReportItem.Create(Country, Type, profit, TaxPercent, tax);
        }

        private decimal CalculateTax(decimal profit, decimal taxPercent)
        {
            return profit > 0
                ? Math.Round(profit * taxPercent / 100, 0)
                : 0;
        }

        private decimal CalculateProfit(Transaction[] transactions)
        {
            var operations = transactions.SelectMany(x => x.Operations).ToArray();

            return CalculateProfit(operations);
        }

        private decimal CalculateProfit(TransactionOperation[] operations)
        {
            return CalculateCreditOrDebit(TransactionOperationType.Credit)
                   - CalculateCreditOrDebit(TransactionOperationType.Debit);

            decimal CalculateCreditOrDebit(TransactionOperationType type)
            {
                return operations
                    .Where(x => x.Type == type)
                    .Sum(x => x.Price * x.Count * x.DollarPrice);
            }
        }
    }
}