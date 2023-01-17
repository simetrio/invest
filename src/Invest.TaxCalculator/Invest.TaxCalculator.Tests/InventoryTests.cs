using System;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Inventories;
using Invest.TaxCalculator.Tests.Utils;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class InventoryTests
    {
        private readonly Inventory _inventory = new();

        [Test]
        public void Calculate()
        {
            var builder = new EntityBuilder()
                    .WithBuySellShare("AC1", DateTime.Now, 9, 150, 71, DateTime.Now, 7, 150, 71, 0.01m)
                    .WithBuySellShare("AC1", DateTime.Now, 5, 150, 71, DateTime.Now, 6, 150, 71, 0.01m)
                    .WithBuyShare("AC1", DateTime.Now, 4, 150, 71, 0.01m)
                    .WithBuyBond("AC2", DateTime.Now, 8, 150, 71, 0.01m);

            var expected = new[]
            {
                new InventoryItem
                {
                    Ticker = "AC1",
                    Count = 5,
                },
                new InventoryItem
                {
                    Ticker = "AC2",
                    Count = 8,
                },
            };

            var actual = _inventory.Calculate(builder.Operations);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}