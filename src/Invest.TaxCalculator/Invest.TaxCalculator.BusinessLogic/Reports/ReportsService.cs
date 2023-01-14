namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class ReportsService
    {
        private readonly ReportsRepository _repository = new();
        private readonly ReportsValidator _validator = new();

        public void Create(Report report)
        {
            _validator.ValidateCreate(report);
            _repository.CreateOrUpdate(new[] {report});
        }

        public Report[] ReadAll()
        {
            return _repository.ReadAll();
        }

        public void Delete(Report report)
        {
            _validator.ValidateDelete(report);
            _repository.Delete(new[] {report});
        }
        
    }
}