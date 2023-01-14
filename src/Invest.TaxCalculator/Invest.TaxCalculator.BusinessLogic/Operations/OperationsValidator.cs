using System.Linq;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    public class OperationsValidator
    {
        private readonly OperationsRepository _repository = new();
        private readonly TransactionsRepository _transactionsRepository = new();

        public void ValidateCreate(Operation operation)
        {
            ValidateFields(operation);
            ValidateNotExists(operation);
        }

        public void ValidateDelete(Operation operation)
        {
            ValidateNotContainsInTransaction(operation);
        }

        private void ValidateFields(Operation operation)
        {
            operation.Id.Should().NotBeNull();

            if (operation.Type == OperationType.Comission)
            {
                operation.ParentId.Should().NotBeNull();
            }

            operation.Ticker.Should().NotBeNull();
            operation.DateTime.Should().NotBe(default);

            if (operation.Type == OperationType.Comission)
            {
                operation.Count.Should().Be(1);
            }
            else
            {
                operation.Count.Should().BePositive();
            }

            operation.Price.Should().BePositive();
            operation.DollarPrice.Should().BePositive();
        }

        private void ValidateNotExists(Operation operation)
        {
            _repository
                .ReadAll()
                .SingleOrDefault(x => x.Equals(operation))!
                .Should()
                .BeNull($"Operation {operation.Id} is already exists");
        }

        private void ValidateNotContainsInTransaction(Operation operation)
        {
            _transactionsRepository
                .ReadAll()
                .SelectMany(x => x.Items)
                .SelectMany(x => x.Operations)
                .FirstOrDefault(x => x.Id == operation.Id)
                .Should()
                .BeNull($"There is operation {operation.Id} in transaction");
        }
    }
}