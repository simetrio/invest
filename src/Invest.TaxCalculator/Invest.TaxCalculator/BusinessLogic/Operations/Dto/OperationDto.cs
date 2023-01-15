using Invest.TaxCalculator.BusinessLogic.Countries;

namespace Invest.TaxCalculator.BusinessLogic.Operations.Dto
{
    public class OperationDto
    {
        public string Id { get; set; }
        
        public string? ParentId { get; set; }
        
        public string Ticker { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public OperationType Type { get; set; }
        
        public int Count { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal DollarPrice { get; set; }
        
        public Country Country { get; set; }
    }
}