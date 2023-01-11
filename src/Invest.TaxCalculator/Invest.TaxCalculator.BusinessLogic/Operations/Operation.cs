using System;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    /// <summary>
    ///     Операция
    /// </summary>
    public class Operation
    {
        /// <summary>
        ///     Дата и время операции
        /// </summary>
        public DateTime DateTime { get; set; }
        
        /// <summary>
        ///     Тип операции
        /// </summary>
        public OperationType Type { get; set; }
    }
}