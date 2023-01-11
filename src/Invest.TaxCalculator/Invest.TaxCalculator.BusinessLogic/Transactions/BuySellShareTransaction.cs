namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Сделка по купле продаже акции
    /// </summary>
    public class BuySellShareTransaction : ITransaction
    {
        public TransactionOperation[] Operations { get; set; }
    }
}