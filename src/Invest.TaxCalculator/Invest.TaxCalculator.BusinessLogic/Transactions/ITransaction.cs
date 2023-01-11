namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Интерфейс для сделок
    /// </summary>
    public interface ITransaction
    {
        TransactionOperation[] Operations { get; set; }
    }
}