using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.BusinessLogic.Providers
{
    /// <summary>
    ///     Провайдер дочерних транзакций
    /// </summary>
    public class ChildOperationsProvider
    {
        private readonly OperationsCollection _operations;

        public ChildOperationsProvider(OperationsCollection operations)
        {
            _operations = operations;
        }
    }
}