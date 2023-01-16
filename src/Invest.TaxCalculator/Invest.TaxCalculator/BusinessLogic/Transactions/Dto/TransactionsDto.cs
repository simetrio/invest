namespace Invest.TaxCalculator.BusinessLogic.Transactions.Dto
{
    public class TransactionsDto
    {
        public int Year { get; set; }

        public TransactionDto[] Items { get; set; }
    }
}