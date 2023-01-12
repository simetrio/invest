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
            var oldBuyShare = Fixture
                .BuildOperation(OperationType.BuyShare)
                .With(x => x.DateTime, new DateTime(2017, 12, 11))
                .With(x => x.DollarPrice, 69.71m)
                .With(x => x.Price, 275.45m)
                .With(x => x.Count, 10)
                .Create();
            
            var oldBuyShareCommission = Fixture
                .BuildCommission(oldBuyShare)
                .With(x => x.DollarPrice, 69.71m)
                .Create();
            
            var oldSellShare = Fixture
                .BuildSell(oldBuyShare)
                .With(x => x.DateTime, new DateTime(2018, 10, 11))
                .With(x => x.DollarPrice, 75.15m)
                .With(x => x.Price, 247.48m)
                .With(x => x.Count, 7)
                .Create();
            
            var oldSellShareCommission = Fixture
                .BuildCommission(oldSellShare)
                .With(x => x.DollarPrice, 75.15m)
                .Create();
            
            var buyShare = Fixture
                .BuildOperation(OperationType.BuyShare)
                .With(x => x.DateTime, new DateTime(2018, 11, 9))
                .With(x => x.DollarPrice, 74.11m)
                .With(x => x.Price, 253.16m)
                .With(x => x.Count, 12)
                .Create();
            
            var buyShareCommission = Fixture
                .BuildCommission(buyShare)
                .With(x => x.DollarPrice, 74.11m)
                .Create();
            
            var sellShare = Fixture
                .BuildSell(oldBuyShare)
                .With(x => x.DateTime, new DateTime(2019, 6, 12))
                .With(x => x.DollarPrice, 73.42m)
                .With(x => x.Price, 289.17m)
                .With(x => x.Count, 9)
                .Create();
            
            var sellShareCommission = Fixture
                .BuildCommission(sellShare)
                .With(x => x.DollarPrice, 73.42m)
                .Create();
            
            var operations = new[]
            {
                oldBuyShare,
                oldBuyShareCommission,
                oldSellShare,
                oldSellShareCommission,
                buyShare,
                buyShareCommission,
                sellShare,
                sellShareCommission,
            };

            var transactions = new Transaction[]
            {
                
            };
            
            var expected = new Transaction[]
            {
                
            };

            return Create(operations, transactions, 2019, expected)
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