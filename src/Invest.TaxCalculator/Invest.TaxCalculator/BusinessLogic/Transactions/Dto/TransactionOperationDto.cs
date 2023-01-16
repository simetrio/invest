namespace Invest.TaxCalculator.BusinessLogic.Transactions.Dto
{
    public class TransactionOperationDto
    {
        public string Id { get; set; }
        
        public TransactionOperationType Type { get; set; }

        public decimal Count { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Price { get; set; }

        public decimal DollarPrice { get; set; }
    }
}