using System;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    /// <summary>
    ///     Операция
    /// </summary>
    public class Operation
    {
        /// <summary>
        ///     Ид операции
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ид родительской итерации
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        ///     Тикер
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        ///     Дата и время операции
        /// </summary>
        public DateTime DateTime { get; set; }
        
        /// <summary>
        ///     Тип операции
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        ///     Кол-во
        /// </summary>
        public int Count { get; set; }
    }
}