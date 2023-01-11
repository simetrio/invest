using System;

namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Калькулятор транзакций
    /// </summary>
    public class TransactionsCalculator
    {
        public TransactionBase[] Calculate(Operation[] operations, TransactionBase[] oldTransactions, int year)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///     Покупка/Продажа/Дивиденды/Комиссия брокера
    /// </summary>
    public class Operation
    {
        
    }

    /// <summary>
    ///     Базовый класс для сделок
    /// </summary>
    public abstract class TransactionBase
    {
        
    }

    /// <summary>
    ///     Сделка по купле продаже
    /// </summary>
    public class BuySellTransaction
    {
        
    }

    /// <summary>
    ///     Дивиденды
    /// </summary>
    public class DividendsTransaction
    {
        
    }
}