using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class Transactions : EntityBase
    {
        public int Year { get; set; }

        public Transaction[] Items { get; set; }

        protected override object[] GetKeys()
        {
            return new object[] {Year};
        }
    }
}