using System;
using Invest.TaxCalculator.BusinessLogic.Countries;
using Invest.TaxCalculator.BusinessLogic.Storage;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    /// <summary>
    ///     Операция
    /// </summary>
    public class Operation : EntityBase
    {
        /// <summary>
        ///     Ид операции
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ид родительской операции
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

        /// <summary>
        ///     Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Курс доллара
        /// </summary>
        public decimal DollarPrice { get; set; }

        /// <summary>
        ///     Страна
        /// </summary>
        public Country Country { get; set; }

        protected override object[] GetKeys()
        {
            return new object[] {Id};
        }
    }
}