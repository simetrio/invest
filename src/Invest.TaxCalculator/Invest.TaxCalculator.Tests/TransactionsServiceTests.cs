using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Reports;
using Invest.TaxCalculator.BusinessLogic.Storage;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.Tests.Utils;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class TransactionsServiceTests
    {
        private readonly TransactionsService _transactionsService = new();
        private readonly ReportsService _reportsService = new();
        private readonly Repository _repository = new();

        private static readonly IFixture Fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _repository.Update(StorageElement.Empty);
        }

        [TestCaseSource(nameof(ValidateFieldsData))]
        public void ValidateFields(Transactions transactions, string field)
        {
            Action action = () => _transactionsService.Create(transactions);
            Func<Transactions[]> actual = () => _transactionsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message.Contains($".{field} "));

            actual().Should().BeEmpty();
        }

        [Test]
        public void ValidateNotExistsYear()
        {
            var builder = new EntityBuilder()
                .WithCouponsTransaction(10, 100, 70);

            var transactions = new Transactions
            {
                Year = 2019,
                Items = builder.Transactions,
            };

            _transactionsService.Create(transactions);

            Action action = () => _transactionsService.Create(transactions);
            Func<Transactions[]> actual = () => _transactionsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Contains(transactions.Year) to be false, but found True"));

            actual().Should().BeEquivalentTo(new[] {transactions});
        }

        [Test]
        public void ValidateExistsPreviewYear()
        {
            var builder = new EntityBuilder()
                .WithCouponsTransaction(10, 100, 70);

            var transactions1 = new Transactions
            {
                Year = 2019,
                Items = builder.Transactions,
            };

            var transactions2 = new Transactions
            {
                Year = 2021,
                Items = builder.Transactions,
            };

            _transactionsService.Create(transactions1);

            Action action = () => _transactionsService.Create(transactions2);
            Func<Transactions[]> actual = () => _transactionsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Contains(transactions.Year - 1) to be true, but found False"));

            actual().Should().BeEquivalentTo(new[] {transactions1});
        }

        [Test]
        public void ValidateNotDeleteWhenHasFutureTransactions()
        {
            var builder = new EntityBuilder()
                .WithCouponsTransaction(10, 100, 70);

            var transactions1 = new Transactions
            {
                Year = 2019,
                Items = builder.Transactions,
            };

            var transactions2 = new Transactions
            {
                Year = 2020,
                Items = builder.Transactions,
            };

            _transactionsService.Create(transactions1);
            _transactionsService.Create(transactions2);

            Action action = () => _transactionsService.Delete(transactions1);
            Func<Transactions[]> actual = () => _transactionsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Count(x => x > transactions.Year) to be 0, but found 1")
                );

            actual().Should().BeEquivalentTo(new[] {transactions1, transactions2});
        }

        [Test]
        public void ValidateNotExistsReport()
        {
            var builder = new EntityBuilder()
                .WithCouponsTransaction(10, 100, 70)
                .WithReportItem();

            var transactions = new Transactions
            {
                Year = 2019,
                Items = builder.Transactions,
            };

            var report = new Report
            {
                Year = 2019,
                Items = builder.ReportItems,
            };

            _transactionsService.Create(transactions);
            _reportsService.Create(report);

            Action action = () => _transactionsService.Delete(transactions);
            Func<Transactions[]> actual = () => _transactionsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Contains(transactions.Year) to be false, but found True")
                );

            actual().Should().BeEquivalentTo(new[] {transactions});
        }

        [Test]
        public void Delete()
        {
            var builder = new EntityBuilder()
                .WithCouponsTransaction(10, 100, 70);

            var transactions = new Transactions
            {
                Year = 2019,
                Items = builder.Transactions,
            };

            _transactionsService.Create(transactions);
            _transactionsService.Delete(transactions);

            var actual = _transactionsService.ReadAll();

            actual.Should().BeEmpty();
        }

        private static IEnumerable<TestCaseData> ValidateFieldsData()
        {
            yield return CreateTransactionOperation(
                x => { },
                nameof(TransactionOperation.Id)
            ).SetName(nameof(TransactionOperation.Id));

            yield return CreateTransactionOperation(
                x => { x.Id = Fixture.Create<string>(); },
                nameof(TransactionOperation.Count)
            ).SetName(nameof(TransactionOperation.Count));

            yield return CreateTransactionOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Count = Fixture.Create<decimal>();
                },
                nameof(TransactionOperation.DateTime)
            ).SetName(nameof(TransactionOperation.DateTime));

            yield return CreateTransactionOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Count = Fixture.Create<decimal>();
                    x.DateTime = Fixture.Create<DateTime>();
                },
                nameof(TransactionOperation.Price)
            ).SetName(nameof(TransactionOperation.Price));

            yield return CreateTransactionOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Count = Fixture.Create<decimal>();
                    x.DateTime = Fixture.Create<DateTime>();
                    x.Price = Fixture.Create<decimal>();
                },
                nameof(TransactionOperation.DollarPrice)
            ).SetName(nameof(TransactionOperation.DollarPrice));

            yield return CreateTransaction(
                x => { },
                nameof(Transaction.Ticker)
            ).SetName(nameof(Transaction.Ticker));

            yield return CreateTransaction(
                x => { x.Ticker = Fixture.Create<string>(); },
                nameof(Transaction.Operations)
            ).SetName(nameof(Transaction.Operations));

            yield return CreateTransactions(
                x => { },
                nameof(Transactions.Year)
            ).SetName(nameof(Transactions.Year));
        }

        private static TestCaseData CreateTransactions(Action<Transactions> update, string filed)
        {
            var transactions = new Transactions();
            update(transactions);

            return new TestCaseData(transactions, filed);
        }

        private static TestCaseData CreateTransaction(Action<Transaction> update, string filed)
        {
            var transaction = new Transaction();
            update(transaction);

            var transactions = new Transactions
            {
                Year = 2019,
                Items = new[] {transaction}
            };

            return new TestCaseData(transactions, filed);
        }

        private static TestCaseData CreateTransactionOperation(Action<TransactionOperation> update, string filed)
        {
            var transactionOperation = new TransactionOperation();
            update(transactionOperation);

            var transaction = new Transaction
            {
                Ticker = "CDB",
                Country = Country.Us,
                Type = TransactionType.SellShareOrBond,
                Operations = new[] {transactionOperation},
            };

            var transactions = new Transactions
            {
                Year = 2019,
                Items = new[] {transaction}
            };

            return new TestCaseData(transactions, filed);
        }
    }
}