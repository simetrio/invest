namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Сделка по купле продаже облигации
    /// </summary>
    public class BuySellBondTransaction : ITransaction
    {
        public TransactionOperation[] Operations { get; set; }
    }
}