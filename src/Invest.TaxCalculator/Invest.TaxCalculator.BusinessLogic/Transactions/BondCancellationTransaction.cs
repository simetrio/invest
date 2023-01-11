namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Гашение облигации
    /// </summary>
    public class BondCancellationTransaction : ITransaction
    {
        public TransactionOperation[] Operations { get; set; }
    }
}