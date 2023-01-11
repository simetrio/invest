using System;
using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Операция в транзакци
    /// </summary>
    public class TransactionOperation
    {
        public string Id { get; set; }

        public decimal Count { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Price { get; set; }

        public decimal DollarPrice { get; set; }

        public static TransactionOperation Create(Operation operation, int count)
        {
            return new TransactionOperation
            {
                Id = operation.Id,
                Count = count,
                DateTime = operation.DateTime,
                Price = operation.Price,
                DollarPrice = operation.DollarPrice,
            };
        }

        public static TransactionOperation CreateCommission(Operation operation, int count)
        {
            return new TransactionOperation
            {
                Id = operation.Id,
                Count = (decimal) count / operation.Count,
                DateTime = operation.DateTime,
                Price = operation.Price,
                DollarPrice = operation.DollarPrice,
            };
        }
    }
}