namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Дивиденды
    /// </summary>
    public class ShareDividendsTransaction : ITransaction
    {
        public TransactionOperation[] Operations { get; set; }
    }
}