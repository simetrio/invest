using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class OperationsValidatorTests
    {
        private readonly OperationsRepository _operationsRepository = new();
        private readonly OperationsValidator _operationsValidator = new();
        private readonly TransactionsRepository _transactionsRepository = new();

        private static readonly IFixture Fixture = new Fixture();

        [TestCaseSource(nameof(ValidateFieldsData))]
        public void ValidateFields_Should_ThrowException(Operation operation, string field)
        {
            Action action = () => _operationsValidator.ValidateCreate(operation);

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message.Contains($".{field} "));
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
                x =>
                {
                    x.Id = Fixture.Create<string>();
                },
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