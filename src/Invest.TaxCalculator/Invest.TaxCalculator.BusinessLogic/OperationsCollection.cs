namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Коллекция операций
    /// </summary>
    public class OperationsCollection
    {
        public OperationsCollection(Operation[] all)
        {
            All = all;
        }

        public Operation[] All { get; set; }
    }
}