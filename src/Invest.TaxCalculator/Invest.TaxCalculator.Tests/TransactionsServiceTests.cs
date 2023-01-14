using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Storage;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class TransactionsServiceTests
    {
        private readonly TransactionsService _transactionsService = new();
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
                x =>
                {
                    x.Ticker = Fixture.Create<string>();
                },
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