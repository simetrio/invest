using System;
using System.Collections.Generic;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.BusinessLogic.Transactions.Calculator;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class TransactionsCalculatorTests
    {
        private readonly TransactionsCalculator _transactionsCalculator = new();

        [TestCaseSource(nameof(TestCalculateData))]
        public void CalculateTransactions(
            Operation[] operationsArray,
            Transaction[] transactionsArray,
            int year,
            IEnumerable<Transaction> expected
        )
        {
            var operations = new OperationsCollection(operationsArray);
            var transactions = new TransactionsCollection(transactionsArray);

            var actual = _transactionsCalculator.Calculate(operations, transactions, year);

            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> TestCalculateData()
        {
            yield return BuySellShared();
        }

        private static TestCaseData BuySellShared()
        {
            var builder = new EntityBuilder()
                .WithBuySellShare(
                    "BCD",
                    new DateTime(2017, 12, 11),
                    10,
                    275.45m,
                    69.71m,
                    new DateTime(2018, 10, 11),
                    7,
                    247.48m,
                    75.15m,
                    0.01m
                )
                .AndBuySellShareTransaction()
                .WithBuySellShare(
                    "BCD",
                    new DateTime(2018, 11, 9),
                    12,
                    253.16m,
                    74.11m,
                    new DateTime(2019, 6, 12),
                    9,
                    289.17m,
                    73.42m,
                    0.01m
                );

            var buy1 = builder.Operations[0];
            var buy1Commission = builder.Operations[1];
            var buy2 = builder.Operations[^4];
            var buy2Commission = builder.Operations[^3];
            var sell = builder.Operations[^2];
            var sellCommission = builder.Operations[^1];

            var operations1 = new[]
            {
                new TransactionOperation
                {
                    Id = buy1.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 3,
                    DateTime = buy1.DateTime,
                    Price = buy1.Price,
                    DollarPrice = buy1.DollarPrice,
                },
                new TransactionOperation
                {
                    Id = buy1Commission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 3 / 10,
                    DateTime = buy1Commission.DateTime,
                    Price = buy1Commission.Price,
                    DollarPrice = buy1Commission.DollarPrice,
                },
                new TransactionOperation
                {
                    Id = sell.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 3,
                    DateTime = sell.DateTime,
                    Price = sell.Price,
                    DollarPrice = sell.DollarPrice,
                },
                new TransactionOperation
                {
                    Id = sellCommission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 3 / 9,
                    DateTime = sellCommission.DateTime,
                    Price = sellCommission.Price,
                    DollarPrice = sellCommission.DollarPrice,
                },
            };
            
            var operations2 = new[]
            {
                new TransactionOperation
                {
                    Id = buy2.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 6,
                    DateTime = buy2.DateTime,
                    Price = buy2.Price,
                    DollarPrice = buy2.DollarPrice,
                },
                new TransactionOperation
                {
                    Id = buy2Commission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 6 / 12,
                    DateTime = buy2Commission.DateTime,
                    Price = buy2Commission.Price,
                    DollarPrice = buy2Commission.DollarPrice,
                },
                new TransactionOperation
                {
                    Id = sell.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 6,
                    DateTime = sell.DateTime,
                    Price = sell.Price,
                    DollarPrice = sell.DollarPrice,
                },
                new TransactionOperation
                {
                    Id = sellCommission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 6 / 9,
                    DateTime = sellCommission.DateTime,
                    Price = sellCommission.Price,
                    DollarPrice = sellCommission.DollarPrice,
                },
            };

            var expected = new[]
            {
                Transaction.Create(sell, TransactionType.SellShareOrBond, operations1),
                Transaction.Create(sell, TransactionType.SellShareOrBond, operations2),
            };

            return Create(builder.Operations, builder.Transactions, 2019, expected)
                .SetName("BuySellShared");
        }

        private static TestCaseData Create(
            Operation[] operationsArray,
            Transaction[] transactionsArray,
            int year,
            IEnumerable<Transaction> expected
        )
        {
            return new TestCaseData(operationsArray, transactionsArray, year, expected);
        }
    }
}