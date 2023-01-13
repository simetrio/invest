using System;
using System.Collections.Generic;
using AutoFixture;
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
        private static readonly IFixture Fixture = new Fixture();

        [TestCaseSource(nameof(TestCalculateData))]
        public void TestCalculate(
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

            var expected = new Transaction[]
            {
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