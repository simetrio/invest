namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    /// <summary>
    ///     Операция в транзакци
    /// </summary>
    public class TransactionOperation
    {
        public TransactionOperation(string id, int count)
        {
            Id = id;
            Count = count;
        }

        public string Id { get; set; }
        
        public int Count { get; set; }
    }
}