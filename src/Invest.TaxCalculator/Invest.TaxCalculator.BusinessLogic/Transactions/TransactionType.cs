namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public enum TransactionType
    {
        /// <summary>
        ///     Продажа акции или облигации
        /// </summary>
        SellShareOrBond,

        /// <summary>
        ///     Дивиденды
        /// </summary>
        Dividends,

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