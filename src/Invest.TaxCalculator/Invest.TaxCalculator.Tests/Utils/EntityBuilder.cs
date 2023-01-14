using System;
using System.Collections.Generic;
using AutoFixture;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;

namespace Invest.TaxCalculator.Tests.Utils
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

        public EntityBuilder WithBuyCancellationBond(
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
                .BuildOperation(OperationType.BondCancellation)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, sellDateTime)
                .With(x => x.Count, sellCount)
                .With(x => x.Price, sellPrice)
                .With(x => x.DollarPrice, sellDollarPrice)
                .Create();

            _operations.Add(buy);
            _operations.Add(buyCommission);
            _operations.Add(sell);

            return this;
        }

        public EntityBuilder WithBuyBond(
            string ticker,
            DateTime dateTime,
            int count,
            decimal price,
            decimal dollarPrice,
            decimal commissionPercent
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.BuyBond)
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

        public EntityBuilder WithCoupons(
            string ticker,
            DateTime dateTime,
            int count,
            decimal price,
            decimal dollarPrice
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.Coupons)
                .With(x => x.Ticker, ticker)
                .With(x => x.DateTime, dateTime)
                .With(x => x.Count, count)
                .With(x => x.Price, price)
                .With(x => x.DollarPrice, dollarPrice)
                .Create();

            _operations.Add(buy);

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
                    Count = (decimal) sell.Count / buy.Count,
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

        public EntityBuilder WithBuySellTransaction(
            int count,
            int totalCount,
            decimal buyPrice,
            decimal buyDollarPrice,
            decimal sellPrice,
            decimal sellDollarPrice,
            decimal commissionPercent
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.BuyShare)
                .With(x => x.Count, count)
                .With(x => x.Price, buyPrice)
                .With(x => x.DollarPrice, buyDollarPrice)
                .Create();
            
            var buyCommission = _fixture
                .BuildCommission(buy)
                .With(x => x.Price, buyPrice * totalCount * commissionPercent)
                .Create();

            var sell = _fixture
                .BuildOperation(OperationType.SellShare)
                .With(x => x.Count, count)
                .With(x => x.Price, sellPrice)
                .With(x => x.DollarPrice, sellDollarPrice)
                .Create();

            var sellCommission = _fixture
                .BuildCommission(sell)
                .With(x => x.Price, sellPrice * totalCount * commissionPercent)
                .Create();

            var operations = new[]
            {
                TransactionOperation.Debit(buy, count),
                TransactionOperation.Commission(buyCommission, count, totalCount),
                TransactionOperation.Credit(sell, count),
                TransactionOperation.Commission(sellCommission, count, totalCount),
            };
            
            var transaction = Transaction.Create(buy, TransactionType.SellShareOrBond, operations);

            _transactions.Add(transaction);

            return this;
        }

        public EntityBuilder WithDividendsTransaction(
            int count,
            decimal price,
            decimal dollarPrice
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.Dividends)
                .With(x => x.Count, count)
                .With(x => x.Price, price)
                .With(x => x.DollarPrice, dollarPrice)
                .Create();

            var operations = new[]
            {
                TransactionOperation.Credit(buy, count),
            };
            
            var transaction = Transaction.Create(buy, TransactionType.Dividends, operations);

            _transactions.Add(transaction);

            return this;
        }

        public EntityBuilder WithCouponsTransaction(
            int count,
            decimal price,
            decimal dollarPrice
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.Coupons)
                .With(x => x.Count, count)
                .With(x => x.Price, price)
                .With(x => x.DollarPrice, dollarPrice)
                .Create();

            var operations = new[]
            {
                TransactionOperation.Credit(buy, count),
            };
            
            var transaction = Transaction.Create(buy, TransactionType.Coupons, operations);

            _transactions.Add(transaction);

            return this;
        }

        public EntityBuilder WithBuyCancellationBondTransaction(
            int count,
            int totalCount,
            decimal buyPrice,
            decimal buyDollarPrice,
            decimal sellPrice,
            decimal sellDollarPrice,
            decimal commissionPercent
        )
        {
            var buy = _fixture
                .BuildOperation(OperationType.BuyBond)
                .With(x => x.Count, count)
                .With(x => x.Price, buyPrice)
                .With(x => x.DollarPrice, buyDollarPrice)
                .Create();

            var buyCommission = _fixture
                .BuildCommission(buy)
                .With(x => x.Price, buyPrice * count * commissionPercent)
                .Create();

            var sell = _fixture
                .BuildOperation(OperationType.BondCancellation)
                .With(x => x.Count, count)
                .With(x => x.Price, sellPrice)
                .With(x => x.DollarPrice, sellDollarPrice)
                .Create();

            var operations = new[]
            {
                TransactionOperation.Debit(buy, count),
                TransactionOperation.Commission(buyCommission, count, totalCount),
                TransactionOperation.Credit(sell, count),
            };
            
            var transaction = Transaction.Create(buy, TransactionType.BondCancellation, operations);

            _transactions.Add(transaction);

            return this;
        }

        public EntityBuilder AndTransaction()
        {
            var operation = _operations[^1];
            
            var operations = new[]
            {
                TransactionOperation.Credit(operation, operation.Count),
            };
            
            var transaction = Transaction.Create(operation, TransactionType.Coupons, operations);

            _transactions.Add(transaction);

            return this;
        }

        public Operation[] Operations => _operations.ToArray();

        public Transaction[] Transactions => _transactions.ToArray();
    }
}