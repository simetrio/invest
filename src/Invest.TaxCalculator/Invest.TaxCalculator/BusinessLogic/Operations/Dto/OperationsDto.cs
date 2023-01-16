namespace Invest.TaxCalculator.BusinessLogic.Operations.Dto
{
    public class OperationsDto
    {
        public Dictionary<int, OperationDto[]> OperationsByYear { get; set; } = new();

        public int[] Years { get; set; } = Array.Empty<int>();
    }
}