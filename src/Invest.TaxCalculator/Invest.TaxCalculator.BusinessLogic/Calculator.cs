using System;

namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Калькулятор транзакций
    /// </summary>
    public class TransactionsCalculator
    {
        public ITransaction[] Calculate(OperationsCollection operations, TransactionsCollection transactions, int year)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///     По методу FIFO отдает покупки
    /// </summary>
    public class BuyOperationsIterator
    {
        private readonly OperationsCollection _operations;
        private readonly TransactionsCollection _transactions;

        public BuyOperationsIterator(OperationsCollection operations, TransactionsCollection transactions)
        {
            _operations = operations;
            _transactions = transactions;
        }
    }

    /// <summary>
    ///     Провайдер дочерних транзакций
    /// </summary>
    public class ChildOperationsProvider
    {
        private readonly OperationsCollection _operations;

        public ChildOperationsProvider(OperationsCollection operations)
        {
            _operations = operations;
        }
    }

    /// <summary>
    ///     Коллекция операций
    /// </summary>
    public class OperationsCollection
    {
    }

    /// <summary>
    ///     Операция
    /// </summary>
    public class Operation
    {
    }

    /// <summary>
    ///     Коллекция транзакций
    /// </summary>
    public class TransactionsCollection
    {
    }

    /// <summary>
    ///     Интерфейс для сделок
    /// </summary>
    public interface ITransaction
    {
    }

    /// <summary>
    ///     Сделка по купле продаже акции
    /// </summary>
    public class BuySellShareTransaction : ITransaction
    {
    }

    /// <summary>
    ///     Дивиденды
    /// </summary>
    public class ShareDividendsTransaction : ITransaction
    {
    }

    /// <summary>
    ///     Сделка по купле продаже облигации
    /// </summary>
    public class BuySellBondTransaction : ITransaction
    {
    }

    /// <summary>
    ///     Гашение облигации
    /// </summary>
    public class BondCancellationTransaction : ITransaction
    {
    }

    /// <summary>
    ///     Купоны
    /// </summary>
    public class BondCouponsTransaction : ITransaction
    {
    }
}