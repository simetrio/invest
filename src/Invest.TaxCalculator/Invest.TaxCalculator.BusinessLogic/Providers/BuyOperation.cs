using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.BusinessLogic.Providers
{
    public class BuyOperation
    {
        public BuyOperation(Operation operation, int count)
        {
            Operation = operation;
            Count = count;
        }

        public Operation Operation { get; }

        public int Count { get; set; }
    }
}