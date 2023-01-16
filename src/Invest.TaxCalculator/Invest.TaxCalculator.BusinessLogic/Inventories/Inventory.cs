using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.BusinessLogic.Inventories
{
    public class Inventory
    {
        public InventoryItem[] Calculate(Operation[] operations)
        {
            var sells = operations
                .Where(x => SellTypes.Contains(x.Type))
                .GroupBy(x => x.Ticker)
                .ToDictionary(
                    x => x.Key,
                    x => x.Sum(o => o.Count)
                );

            return operations
                .Where(x => BuyTypes.Contains(x.Type))
                .GroupBy(x => x.Ticker)
                .Select(x => new
                {
                    Ticker = x.Key,
                    Buy = x.Sum(o => o.Count),
                    Sell = sells.TryGetValue(x.Key, out var sell) ? sell : 0,
                })
                .Select(x => new InventoryItem
                {
                    Ticker = x.Ticker,
                    Count = x.Buy - x.Sell,
                })
                .Where(x => x.Count != 0)
                .ToArray();
        }

        private static readonly OperationType[] BuyTypes =
        {
            OperationType.BuyShare,
            OperationType.BuyBond,
        };

        private static readonly OperationType[] SellTypes =
        {
            OperationType.SellShare,
            OperationType.SellBond,
            OperationType.BondCancellation,
        };
    }
}