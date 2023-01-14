namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class TransactionsService
    {
        private readonly TransactionsRepository _repository = new();
        private readonly TransactionsValidator _validator = new();

        public void Create(Transactions transactions)
        {
            _validator.ValidateCreate(transactions);
            _repository.CreateOrUpdate(new[] {transactions});
        }

        public Transactions[] ReadAll()
        {
            return _repository.ReadAll();
        }

        public void Delete(Transactions transactions)
        {
            _validator.ValidateDelete(transactions);
            _repository.Delete(new[] {transactions});
        }
        
    }
}