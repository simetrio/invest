using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.BusinessLogic.Transactions.Calculator;
using Invest.TaxCalculator.Tests.Utils;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class TransactionsCalculatorTests
    {
        private readonly TransactionsCalculator _transactionsCalculator = new();

        [TestCaseSource(nameof(TestCalculateData))]
        public void CalculateTransactions(
            Operation[] operations,
            Transaction[] transactions,
            int year,
            Transactions expected
        )
        {
            var actual = _transactionsCalculator.Calculate(operations, transactions, year);

            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> TestCalculateData()
        {
            yield return BuySellShare();
            yield return Dividends();
            yield return BuySellBond();
            yield return CancellationBond();
            yield return Coupons();
        }

        private static TestCaseData BuySellShare()
        {
            var builder = new EntityBuilder()
                .WithBuySellShare(
                    "BCD",
                    new DateTime(2017, 12, 11),
                    8,
                    275.45m,
                    69.71m,
                    new DateTime(2018, 10, 11),
                    5,
                    247.48m,
                    75.15m,
                    0.01m
                )
                .AndBuySellTransaction()
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
                    DateTime = new DateTime(2017, 12, 11),
                    Price = 275.45m,
                    DollarPrice = 69.71m,
                },
                new TransactionOperation
                {
                    Id = buy1Commission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 3 / 8,
                    DateTime = new DateTime(2017, 12, 11),
                    Price = 22.036m,
                    DollarPrice = 69.71m,
                },
                new TransactionOperation
                {
                    Id = sell.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 3,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 289.17m,
                    DollarPrice = 73.42m,
                },
                new TransactionOperation
                {
                    Id = sellCommission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 3 / 9,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 26.0253m,
                    DollarPrice = 73.42m,
                },
            };

            var operations2 = new[]
            {
                new TransactionOperation
                {
                    Id = buy2.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 6,
                    DateTime = new DateTime(2018, 11, 9),
                    Price = 253.16m,
                    DollarPrice = 74.11m,
                },
                new TransactionOperation
                {
                    Id = buy2Commission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 6 / 12,
                    DateTime = new DateTime(2018, 11, 9),
                    Price = 30.3792m,
                    DollarPrice = 74.11m,
                },
                new TransactionOperation
                {
                    Id = sell.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 6,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 289.17m,
                    DollarPrice = 73.42m,
                },
                new TransactionOperation
                {
                    Id = sellCommission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 6 / 9,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 26.0253m,
                    DollarPrice = 73.42m,
                },
            };

            var expected = new[]
            {
                Transaction.Create(sell, TransactionType.SellShareOrBond, operations1),
                Transaction.Create(sell, TransactionType.SellShareOrBond, operations2),
            };

            return Create(builder, 2019, expected).SetName("BuySellShare");
        }

        private static TestCaseData Dividends()
        {
            var builder = new EntityBuilder()
                .WithBuyShare(
                    "ERV",
                    new DateTime(2017, 12, 11),
                    10,
                    275.45m,
                    69.71m,
                    0.01m
                )
                .WithDividends(
                    "ERV",
                    new DateTime(2018, 11, 9),
                    10,
                    15.74m,
                    74.11m
                )
                .WithDividends(
                    "ERV",
                    new DateTime(2019, 3, 15),
                    10,
                    17.74m,
                    73.21m
                )
                .WithDividends(
                    "ERV",
                    new DateTime(2020, 1, 15),
                    10,
                    18.15m,
                    73.11m
                );

            var operations = new[]
            {
                new TransactionOperation
                {
                    Id = builder.Operations[^2].Id,
                    Type = TransactionOperationType.Credit,
                    Count = 10,
                    DateTime = new DateTime(2019, 3, 15),
                    Price = 17.74m,
                    DollarPrice = 73.21m,
                },
            };

            var expected = new[]
            {
                Transaction.Create(builder.Operations[0], TransactionType.Dividends, operations),
            };

            return Create(builder, 2019, expected).SetName("Dividends");
        }

        private static TestCaseData BuySellBond()
        {
            var builder = new EntityBuilder()
                .WithBuySellBond(
                    "R48948",
                    new DateTime(2017, 12, 11),
                    10,
                    275.45m,
                    69.71m,
                    new DateTime(2018, 10, 11),
                    10,
                    247.48m,
                    75.15m,
                    0.01m
                )
                .AndBuySellTransaction()
                .WithBuySellBond(
                    "R48948",
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

            var buy = builder.Operations[^4];
            var buyCommission = builder.Operations[^3];
            var sell = builder.Operations[^2];
            var sellCommission = builder.Operations[^1];

            var operations = new[]
            {
                new TransactionOperation
                {
                    Id = buy.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 9,
                    DateTime = new DateTime(2018, 11, 9),
                    Price = 253.16m,
                    DollarPrice = 74.11m,
                },
                new TransactionOperation
                {
                    Id = buyCommission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 9 / 12,
                    DateTime = new DateTime(2018, 11, 9),
                    Price = 30.3792m,
                    DollarPrice = 74.11m,
                },
                new TransactionOperation
                {
                    Id = sell.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 9,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 289.17m,
                    DollarPrice = 73.42m,
                },
                new TransactionOperation
                {
                    Id = sellCommission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 1,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 26.0253m,
                    DollarPrice = 73.42m,
                },
            };

            var expected = new[]
            {
                Transaction.Create(sell, TransactionType.SellShareOrBond, operations),
            };

            return Create(builder, 2019, expected).SetName("BuySellBond");
        }

        private static TestCaseData CancellationBond()
        {
            var builder = new EntityBuilder()
                .WithBuySellBond(
                    "R48948",
                    new DateTime(2017, 12, 11),
                    8,
                    285.45m,
                    69.71m,
                    new DateTime(2018, 10, 11),
                    6,
                    289.25m,
                    75.15m,
                    0.01m
                )
                .AndBuySellTransaction()
                .WithBuyCancellationBond(
                    "R48948",
                    new DateTime(2018, 11, 9),
                    12,
                    291.86m,
                    74.11m,
                    new DateTime(2019, 6, 12),
                    14,
                    300m,
                    73.42m,
                    0.01m
                );

            var buy1 = builder.Operations[0];
            var buy1Commission = builder.Operations[1];
            var buy2 = builder.Operations[^3];
            var buy2Commission = builder.Operations[^2];
            var cancellation = builder.Operations[^1];

            var operations1 = new[]
            {
                new TransactionOperation
                {
                    Id = buy1.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 2,
                    DateTime = new DateTime(2017, 12, 11),
                    Price = 285.45m,
                    DollarPrice = 69.71m,
                },
                new TransactionOperation
                {
                    Id = buy1Commission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = (decimal) 2 / 8,
                    DateTime = new DateTime(2017, 12, 11),
                    Price = 22.836m,
                    DollarPrice = 69.71m,
                },
                new TransactionOperation
                {
                    Id = cancellation.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 2,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 300m,
                    DollarPrice = 73.42m,
                },
            };

            var operations2 = new[]
            {
                new TransactionOperation
                {
                    Id = buy2.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 12,
                    DateTime = new DateTime(2018, 11, 9),
                    Price = 291.86m,
                    DollarPrice = 74.11m,
                },
                new TransactionOperation
                {
                    Id = buy2Commission.Id,
                    Type = TransactionOperationType.Debit,
                    Count = 1,
                    DateTime = new DateTime(2018, 11, 9),
                    Price = 35.0232m,
                    DollarPrice = 74.11m,
                },
                new TransactionOperation
                {
                    Id = cancellation.Id,
                    Type = TransactionOperationType.Credit,
                    Count = 12,
                    DateTime = new DateTime(2019, 6, 12),
                    Price = 300m,
                    DollarPrice = 73.42m,
                },
            };

            var expected = new[]
            {
                Transaction.Create(cancellation, TransactionType.BondCancellation, operations1),
                Transaction.Create(cancellation, TransactionType.BondCancellation, operations2),
            };

            return Create(builder, 2019, expected).SetName("BondCancellation");
        }

        private static TestCaseData Coupons()
        {
            var builder = new EntityBuilder()
                .WithBuyBond(
                    "R48948",
                    new DateTime(2017, 12, 11),
                    10,
                    275.45m,
                    69.71m,
                    0.01m
                )
                .WithCoupons(
                    "R48948",
                    new DateTime(2018, 11, 9),
                    10,
                    15.74m,
                    74.11m
                )
                .WithCoupons(
                    "R48948",
                    new DateTime(2019, 3, 15),
                    10,
                    17.74m,
                    73.21m
                )
                .WithCoupons(
                    "R48948",
                    new DateTime(2020, 1, 15),
                    10,
                    18.15m,
                    73.11m
                );

            var operations = new[]
            {
                new TransactionOperation
                {
                    Id = builder.Operations[^2].Id,
                    Type = TransactionOperationType.Credit,
                    Count = 10,
                    DateTime = new DateTime(2019, 3, 15),
                    Price = 17.74m,
                    DollarPrice = 73.21m,
                },
            };

            var expected = new[]
            {
                Transaction.Create(builder.Operations[0], TransactionType.Coupons, operations),
            };

            return Create(builder, 2019, expected).SetName("Coupons");
        }

        private static TestCaseData Create(
            EntityBuilder entityBuilder,
            int year,
            IEnumerable<Transaction> expectedTransactions
        )
        {
            var expected = new Transactions
            {
                Year = year,
                Items = expectedTransactions.ToArray(),
            };

            return new TestCaseData(entityBuilder.Operations, entityBuilder.Transactions, year, expected);
        }
    }
}