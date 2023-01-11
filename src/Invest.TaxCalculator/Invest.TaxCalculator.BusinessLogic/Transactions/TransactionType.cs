namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public enum TransactionType
    {
        /// <summary>
        ///     Продажа акции
        /// </summary>
        SellShare,

        /// <summary>
        ///     Дивиденды
        /// </summary>
        Dividends,

        /// <summary>
        ///     Продажа облигации
        /// </summary>
        SellBond,

        /// <summary>
        ///     Гашение облигации
        /// </summary>
        BondCancellation,
        
        /// <summary>
        ///     Купон
        /// </summary>
        Coupons,
    }
}