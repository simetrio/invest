namespace Invest.TaxCalculator.BusinessLogic
{
    /// <summary>
    ///     Тип операции
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        ///     Покупка акции
        /// </summary>
        BuyShare,
        
        /// <summary>
        ///     Продажа акции
        /// </summary>
        SellShare,
        
        /// <summary>
        ///     Дивиденды
        /// </summary>
        Dividends,
        
        /// <summary>
        ///     Покупка облигации
        /// </summary>
        BuyBond,
        
        /// <summary>
        ///     Продажа облигации
        /// </summary>
        SellBond,
        
        /// <summary>
        ///     Гашение облигации
        /// </summary>
        BondCancellation,
        
        /// <summary>
        ///     Купоны
        /// </summary>
        Coupons,
        
        /// <summary>
        ///     Комиссия
        /// </summary>
        Comission,
    }
}