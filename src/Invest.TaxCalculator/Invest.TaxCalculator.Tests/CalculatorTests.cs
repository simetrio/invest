using System.Collections.Generic;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Calculate;
using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.Tests.Utils;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator = new();

        [TestCaseSource(nameof(TestCalculateData))]
        public void Calculate(
            Transaction[] transactions,
            int year,
            Report expected
        )
        {
            var actual = _calculator.Calculate(transactions, year);

            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> TestCalculateData()
        {
            yield return BuySellPlus();
        }

        private static TestCaseData BuySellPlus()
        {
            var builder = new EntityBuilder()
                .WithBuySellTransaction(
                    9,
                    17,
                    249.15m,
                    71.15m,
                    257.98m,
                    74.84m,
                    0.01m
                )
                .WithBuySellTransaction(
                    14,
                    32,
                    149.15m,
                    72.15m,
                    157.98m,
                    72.84m,
                    0.01m
                );

            var expected = new Report
            {
                Year = 2018,
                Items = new[]
                {
                    new ReportItem
                    {
                        Country = Country.Us,
                        Type = TransactionType.SellShareOrBond,
                        // 9 * 257.98m * 74.84m - 9 * 249.15m * 71.15m - 9 * 249.15m * 71.15m * 0.01m - 9 * 257.98m * 74.84m * 0.01m
                        // 14 * 157.98m * 72.84m - 14 * 149.15m * 72.15m - 14 * 149.15m * 72.15m * 0.01m - 14 * 157.98m * 72.84m * 0.01m
                        // 173765,0088 - 159543,2025 - 1595,432025 - 1737,650088 = 10888,724187
                        // 161101,6848 - 150656,415 - 1506,56415 - 1611,016848 = 7327,688802
                        Profit = 18216.412989m,
                        TaxPercent = 13m,
                        Tax = 2368m,
                    }
                }
            };

            return Create(builder, 2018, expected);
        }

        private static TestCaseData Create(
            EntityBuilder entityBuilder,
            int year,
            Report expected
        )
        {
            return new TestCaseData(entityBuilder.Transactions, year, expected);
        }
    }
}