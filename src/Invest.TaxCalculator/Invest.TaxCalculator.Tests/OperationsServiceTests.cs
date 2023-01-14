using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Storage;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.BusinessLogic.Utils;
using Invest.TaxCalculator.Tests.Utils;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class OperationsServiceTests
    {
        private readonly OperationsService _operationsService = new();
        private readonly TransactionsService _transactionsService = new();
        private readonly Repository _repository = new();

        private static readonly IFixture Fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _repository.Update(StorageElement.Empty);
        }

        [TestCaseSource(nameof(ValidateFieldsData))]
        public void ValidateFields(Operation operation, string field)
        {
            Action action = () => _operationsService.Create(operation);
            Func<Operation[]> actual = () => _operationsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message.Contains($".{field} "));
            
            actual().Should().BeEmpty();
        }

        [Test]
        public void ValidateNotExists()
        {
            var builder = new EntityBuilder()
                .WithCoupons(
                    "R486",
                    new DateTime(2018, 12, 16),
                    10,
                    456,
                    74
                );
            var operation = builder.Operations[0];

            builder.Operations.ForEach(_operationsService.Create);

            Action action = () => _operationsService.Create(operation);
            Func<Operation[]> actual = () => _operationsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message.Contains(operation.Id));
            
            actual().Should().BeEquivalentTo(new[] {operation});
        }

        [Test]
        public void ValidateNotContainsInTransaction()
        {
            var builder = new EntityBuilder()
                .WithCoupons(
                    "R486",
                    new DateTime(2018, 12, 16),
                    10,
                    456,
                    74
                )
                .AndTransaction();
            var operation = builder.Operations[0];
            var transactions = new Transactions
            {
                Year = 2019,
                Items = builder.Transactions,
            };

            builder.Operations.ForEach(_operationsService.Create);
            _transactionsService.Create(transactions);

            Action action = () => _operationsService.Delete(operation);
            Func<Operation[]> actual = () => _operationsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message.Contains(operation.Id));

            actual().Should().BeEquivalentTo(new[] {operation});
        }

        [Test]
        public void Delete()
        {
            var builder = new EntityBuilder()
                .WithCoupons(
                    "R486",
                    new DateTime(2018, 12, 16),
                    10,
                    456,
                    74
                );
            var operation = builder.Operations[0];

            builder.Operations.ForEach(_operationsService.Create);

            _operationsService.Delete(operation);
            var actual = _operationsService.ReadAll();

            actual.Should().BeEmpty();
        }

        private static IEnumerable<TestCaseData> ValidateFieldsData()
        {
            yield return CreateOperation(
                x => { },
                nameof(Operation.Id)
            ).SetName(nameof(Operation.Id));

            yield return CreateOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Type = OperationType.Comission;
                },
                nameof(Operation.ParentId)
            ).SetName(nameof(Operation.ParentId));

            yield return CreateOperation(
                x => { x.Id = Fixture.Create<string>(); },
                nameof(Operation.Ticker)
            ).SetName(nameof(Operation.Ticker));

            yield return CreateOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Ticker = Fixture.Create<string>();
                },
                nameof(Operation.DateTime)
            ).SetName(nameof(Operation.DateTime));

            yield return CreateOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.ParentId = Fixture.Create<string>();
                    x.Ticker = Fixture.Create<string>();
                    x.DateTime = Fixture.Create<DateTime>();
                    x.Type = OperationType.Comission;
                    x.Count = Fixture.Create<int>();
                },
                nameof(Operation.Count)
            ).SetName(nameof(Operation.Count));

            yield return CreateOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Ticker = Fixture.Create<string>();
                    x.DateTime = Fixture.Create<DateTime>();
                },
                nameof(Operation.Count)
            ).SetName(nameof(Operation.Count));

            yield return CreateOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Ticker = Fixture.Create<string>();
                    x.DateTime = Fixture.Create<DateTime>();
                    x.Count = Fixture.Create<int>();
                },
                nameof(Operation.Price)
            ).SetName(nameof(Operation.Price));

            yield return CreateOperation(
                x =>
                {
                    x.Id = Fixture.Create<string>();
                    x.Ticker = Fixture.Create<string>();
                    x.DateTime = Fixture.Create<DateTime>();
                    x.Count = Fixture.Create<int>();
                    x.Price = Fixture.Create<decimal>();
                },
                nameof(Operation.DollarPrice)
            ).SetName(nameof(Operation.DollarPrice));
        }

        private static TestCaseData CreateOperation(Action<Operation> update, string filed)
        {
            var operation = new Operation();
            update(operation);

            return new TestCaseData(operation, filed);
        }
    }
}