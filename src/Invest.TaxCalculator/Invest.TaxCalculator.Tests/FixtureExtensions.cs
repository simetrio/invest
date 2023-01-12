using AutoFixture;
using AutoFixture.Dsl;
using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.Tests
{
    public static class FixtureExtensions
    {
        public static IPostprocessComposer<Operation> BuildOperation(this IFixture fixture, OperationType operationType)
        {
            return fixture
                .Build<Operation>()
                .With(x => x.Type, operationType)
                .With(x => x.DollarPrice, 69.71m)
                .With(x => x.ParentId, (string?)null);
        }
        
        public static IPostprocessComposer<Operation> BuildCommission(this IFixture fixture, Operation operation)
        {
            return fixture
                .BuildOperation(OperationType.Comission)
                .With(x => x.Ticker, operation.Ticker)
                .With(x => x.ParentId, operation.Id)
                .With(x => x.DateTime, operation.DateTime)
                .With(x => x.Count, 1);
        }
        
        public static IPostprocessComposer<Operation> BuildSell(this IFixture fixture, Operation operation)
        {
            return fixture
                .BuildOperation(OperationType.Comission)
                .With(x => x.Ticker, operation.Ticker);
        }
    }
}