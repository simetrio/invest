using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class Report : EntityBase
    {
        public int Year { get; set; }

        public ReportItem[] Items { get; set; }

        protected override object[] GetKeys()
        {
            return new object[] {Year};
        }
    }
}