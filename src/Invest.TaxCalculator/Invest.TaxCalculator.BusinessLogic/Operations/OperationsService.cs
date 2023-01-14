namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    public class OperationsService
    {
        private readonly OperationsRepository _repository = new();
        private readonly OperationsValidator _validator = new();

        public void Create(Operation operation)
        {
            _validator.ValidateCreate(operation);
            _repository.CreateOrUpdate(new[] {operation});
        }

        public Operation[] ReadAll()
        {
            return _repository.ReadAll();
        }

        public void Delete(Operation operation)
        {
            _validator.ValidateDelete(operation);
            _repository.Delete(new[] {operation});
        }
    }
}