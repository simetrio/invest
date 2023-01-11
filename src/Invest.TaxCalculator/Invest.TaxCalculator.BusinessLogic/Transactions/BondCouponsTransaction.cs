namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Купоны
    /// </summary>
    public class BondCouponsTransaction : ITransaction
    {
        public TransactionOperation[] Operations { get; set; }
    }
}