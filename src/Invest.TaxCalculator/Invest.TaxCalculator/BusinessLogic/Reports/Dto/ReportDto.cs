namespace Invest.TaxCalculator.BusinessLogic.Reports.Dto
{
    public class ReportDto
    {
        public int Year { get; set; }

        public ReportItemDto[] Items { get; set; }
    }
}