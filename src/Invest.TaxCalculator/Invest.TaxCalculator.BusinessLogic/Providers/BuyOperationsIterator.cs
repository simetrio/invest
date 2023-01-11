using System;
using System.Collections.Generic;
using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.BusinessLogic.Providers
{
    /// <summary>
    ///     По методу FIFO отдает покупки
    /// </summary>
    public class BuyOperationsIterator
    {
        private readonly BuyTickerOperationsIterator[] _operationsIterator;

        public BuyOperationsIterator(OperationsCollection operations, TransactionsCollection transactions)
        {
            _operationsIterator = new BuyTickerOperationsIteratorFactory().Create(operations, transactions);
        }

        public IEnumerable<TransactionOperation> Get(Operation operation)
        {
            var iterator = _operationsIterator.Single(x => x.IsTicker(operation.Ticker));

            return iterator.Get(operation);
        }

        private class BuyTickerOperationsIteratorFactory
        {
            public BuyTickerOperationsIterator[] Create(
                OperationsCollection operations,
                TransactionsCollection transactions
            )
            {
                var usedOperationCounts = transactions
                    .All
                    .SelectMany(x => x.Operations)
                    .ToLookup(x => x.Id, x => x.Count);

                return operations
                    .All
                    .Where(NeedOperation)
                    .OrderBy(x => x.DateTime)
                    .Select(x =>
                    {
                        var count = x.Count - usedOperationCounts[x.Id].Sum();
                        return new OperationRest(x, count);
                    })
                    .GroupBy(x => x.Operation.Ticker)
                    .Select(x => new BuyTickerOperationsIterator(x.Key, x.ToArray()))
                    .ToArray();
            }

            private bool NeedOperation(Operation operation)
            {
                return operation.Type switch
                {
                    OperationType.BuyShare
                        or OperationType.BuyBond => true,

                    OperationType.SellShare
                        or OperationType.Dividends
                        or OperationType.SellBond
                        or OperationType.BondCancellation
                        or OperationType.Coupons
                        or OperationType.Comission => false,

                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private class BuyTickerOperationsIterator
        {
            private readonly OperationRest[] _operationRests;
            private readonly string _ticker;

            public BuyTickerOperationsIterator(string ticker, OperationRest[] operationRests)
            {
                _ticker = ticker;
                _operationRests = operationRests;
            }

            public bool IsTicker(string ticker)
            {
                return _ticker == ticker;
            }

            public IEnumerable<TransactionOperation> Get(Operation operation)
            {
                var count = operation.Count;

                foreach (var operationRest in _operationRests)
                {
                    if (operationRest.Count == 0) continue;

                    if (operationRest.Count <= count)
                    {
                        var rest = new TransactionOperation(operationRest.Operation.Id, operationRest.Count);
                        count -= operationRest.Count;
                        operationRest.Count = 0;

                        yield return rest;
                    }
                    else
                    {
                        var rest = new TransactionOperation(operationRest.Operation.Id, count);
                        operationRest.Count -= count;
                        count = 0;

                        yield return rest;
                    }

                    if (count == 0) yield break;
                }

                throw new Exception($"Не нашли часть покупок бумаги {operation.Ticker} в кол-ве {count}");
            }
        }

        private class OperationRest
        {
            public OperationRest(Operation operation, int count)
            {
                Operation = operation;
                Count = count;
            }

            public Operation Operation { get; }

            public int Count { get; set; }
        }
    }
}