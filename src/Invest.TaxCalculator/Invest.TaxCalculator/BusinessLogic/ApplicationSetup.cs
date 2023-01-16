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
            var fileInfo = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Repository.FileName = "TaxCalculator.txt";
            Repository.DirectoryName = fileInfo.DirectoryName!;
        }
    }
}