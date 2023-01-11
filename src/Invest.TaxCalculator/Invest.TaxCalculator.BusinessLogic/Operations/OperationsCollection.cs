namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    /// <summary>
    ///     Коллекция операций
    /// </summary>
    public class OperationsCollection
    {
        public OperationsCollection(Operation[] operations)
        {
            All = operations;
        }

        public Operation[] All { get; set; }
    }
}