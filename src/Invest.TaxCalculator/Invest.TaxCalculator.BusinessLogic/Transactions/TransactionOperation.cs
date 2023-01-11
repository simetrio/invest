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
        
        public TransactionOperationType Type { get; set; }

        public decimal Count { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Price { get; set; }

        public decimal DollarPrice { get; set; }

        /// <summary>
        ///     Расход
        /// </summary>
        public static TransactionOperation Debit(Operation operation, int count)
        {
            return new TransactionOperation
            {
                Id = operation.Id,
                Type = TransactionOperationType.Debit,
                Count = count,
                DateTime = operation.DateTime,
                Price = operation.Price,
                DollarPrice = operation.DollarPrice,
            };
        }

        /// <summary>
        ///     Приход
        /// </summary>
        public static TransactionOperation Credit(Operation operation, int count)
        {
            return new TransactionOperation
            {
                Id = operation.Id,
                Type = TransactionOperationType.Credit,
                Count = count,
                DateTime = operation.DateTime,
                Price = operation.Price,
                DollarPrice = operation.DollarPrice,
            };
        }

        /// <summary>
        ///     Комиссия
        /// </summary>
        public static TransactionOperation Commission(Operation operation, int count, int totalCount)
        {
            return new TransactionOperation
            {
                Id = operation.Id,
                Type = TransactionOperationType.Debit,
                Count = (decimal) count / totalCount,
                DateTime = operation.DateTime,
                Price = operation.Price,
                DollarPrice = operation.DollarPrice,
            };
        }
    }
}