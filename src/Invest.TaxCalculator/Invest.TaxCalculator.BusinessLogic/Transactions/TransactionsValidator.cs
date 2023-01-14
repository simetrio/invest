using System.Linq;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Reports;
using Invest.TaxCalculator.BusinessLogic.Utils;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class TransactionsValidator
    {
        private readonly TransactionsRepository _repository = new();
        private readonly ReportsRepository _reportsRepository = new();

        public void ValidateCreate(Transactions transactions)
        {
            ValidateFields(transactions);
            ValidateValidCreateYear(transactions);
        }

        public void ValidateDelete(Transactions transactions)
        {
            ValidateValidDeleteYear(transactions);
            ValidateNotExistsReport(transactions);
        }

        private void ValidateFields(Transactions transactions)
        {
            transactions.Year.Should().BePositive();
            transactions.Items.ForEach(ValidateFields);
        }

        private void ValidateFields(Transaction transaction)
        {
            transaction.Ticker.Should().NotBeNull();
            transaction.Operations.Should().NotBeEmpty();
            transaction.Operations.ForEach(ValidateFields);
        }

        private void ValidateFields(TransactionOperation transactionOperation)
        {
            transactionOperation.Id.Should().NotBeNull();
            transactionOperation.Count.Should().BePositive();
            transactionOperation.DateTime.Should().NotBe(default);
            transactionOperation.Price.Should().BePositive();
            transactionOperation.DollarPrice.Should().BePositive();
        }

        private void ValidateValidCreateYear(Transactions transactions)
        {
            var existYears = _repository
                .ReadAll()
                .Select(x => x.Year)
                .ToArray();

            if (!existYears.Any())
            {
                return;
            }

            existYears.Contains(transactions.Year).Should().BeFalse();
            existYears.Contains(transactions.Year - 1).Should().BeTrue();
        }
        
        private void ValidateValidDeleteYear(Transactions transactions)
        {
            var existYears = _repository
                .ReadAll()
                .Select(x => x.Year)
                .ToArray();
            
            existYears.Count(x => x > transactions.Year).Should().Be(0);
        }
        
        private void ValidateNotExistsReport(Transactions transactions)
        {
            var existYears = _reportsRepository
                .ReadAll()
                .Select(x => x.Year)
                .ToArray();
            
            existYears.Contains(transactions.Year).Should().BeFalse();
        }
    }
}