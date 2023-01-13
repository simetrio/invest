using System;
using System.Collections.Generic;
using AutoFixture;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.Tests
{
    public class EntityBuilder
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly List<Operation> _operations = new();
        private readonly List<Transaction> _transactions = new();

        public EntityBuilder WithBuySellShare(
            string ticker,
            DateTime buyDateTime,
            int buyCount,
            decimal buyPrice,
            decimal buyDollarPrice,
            DateTime sellDateTime,
            int sellCount,
            decimal sellPrice,
            decimal sellDollarPrice,
            decimal commissionPercent
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.BuyShare)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, buyDateTime)
                .With(x => x.Count, buyCount)
                .With(x => x.Price, buyPrice)
                .With(x => x.DollarPrice, buyDollarPrice)
                .Create();

            var buyCommission = _fixture
                .BuildCommission(buy)
                .With(x => x.Price, buyPrice * buyCount * commissionPercent)
                .Create();

            var sell = _fixture
                .BuildOperation(OperationType.SellShare)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, sellDateTime)
                .With(x => x.Count, sellCount)
                .With(x => x.Price, sellPrice)
                .With(x => x.DollarPrice, sellDollarPrice)
                .Create();

            var sellCommission = _fixture
                .BuildCommission(sell)
                .With(x => x.Price, sellPrice * sellCount * commissionPercent)
                .Create();
            
            _operations.Add(buy);
            _operations.Add(buyCommission);
            _operations.Add(sell);
            _operations.Add(sellCommission);

            return this;
        }

        public EntityBuilder WithBuyShare(
            string ticker,
            DateTime dateTime,
            int count,
            decimal price,
            decimal dollarPrice,
            decimal commissionPercent
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.BuyShare)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, dateTime)
                .With(x => x.Count, count)
                .With(x => x.Price, price)
                .With(x => x.DollarPrice, dollarPrice)
                .Create();

            var buyCommission = _fixture
                .BuildCommission(buy)
                .With(x => x.Price, price * count * commissionPercent)
                .Create();
            
            _operations.Add(buy);
            _operations.Add(buyCommission);

            return this;
        }

        public EntityBuilder WithDividends(
            string ticker,
            DateTime dateTime,
            int count,
            decimal price,
            decimal dollarPrice
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.Dividends)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, dateTime)
                .With(x => x.Count, count)
                .With(x => x.Price, price)
                .With(x => x.DollarPrice, dollarPrice)
                .Create();
            
            _operations.Add(buy);

            return this;
        }
        
        public EntityBuilder WithBuySellBond(
            string ticker,
            DateTime buyDateTime,
            int buyCount,
            decimal buyPrice,
            decimal buyDollarPrice,
            DateTime sellDateTime,
            int sellCount,
            decimal sellPrice,
            decimal sellDollarPrice,
            decimal commissionPercent
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.BuyBond)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, buyDateTime)
                .With(x => x.Count, buyCount)
                .With(x => x.Price, buyPrice)
                .With(x => x.DollarPrice, buyDollarPrice)
                .Create();

            var buyCommission = _fixture
                .BuildCommission(buy)
                .With(x => x.Price, buyPrice * buyCount * commissionPercent)
                .Create();

            var sell = _fixture
                .BuildOperation(OperationType.SellBond)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, sellDateTime)
                .With(x => x.Count, sellCount)
                .With(x => x.Price, sellPrice)
                .With(x => x.DollarPrice, sellDollarPrice)
                .Create();

            var sellCommission = _fixture
                .BuildCommission(sell)
                .With(x => x.Price, sellPrice * sellCount * commissionPercent)
                .Create();
            
            _operations.Add(buy);
            _operations.Add(buyCommission);
            _operations.Add(sell);
            _operations.Add(sellCommission);

            return this;
        }

        public EntityBuilder AndBuySellTransaction()
        {
            var buy = _operations[^4];
            var buyCommission = _operations[^3];
            var sell = _operations[^2];
            var sellCommission = _operations[^1];

            var operations = new[]
            {
                new TransactionOperation
                {
                    Id = buy.Id,
                    Count = sell.Count,
                },
                new TransactionOperation
                {
                    Id = buyCommission.Id,
                    Count = (decimal)sell.Count / buy.Count,
                },
                new TransactionOperation
                {
                    Id = sell.Id,
                    Count = sell.Count,
                },
                new TransactionOperation
                {
                    Id = sellCommission.Id,
                    Count = 1,
                },
            };

            var transaction = Transaction.Create(buy, TransactionType.SellShareOrBond, operations);
            
            _transactions.Add(transaction);

            return this;
        }

        public Operation[] Operations => _operations.ToArray();
        
        public Transaction[] Transactions => _transactions.ToArray();
    }
}