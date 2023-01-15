using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic
{
    public static class ApplicationSetup
    {
        public static void SetupApplication(this IApplicationBuilder _)
        {
            SetupRepository();
        }

        private static void SetupRepository()
        {
            Repository.FileName = "TaxCalculator.txt";
        }
    }
}