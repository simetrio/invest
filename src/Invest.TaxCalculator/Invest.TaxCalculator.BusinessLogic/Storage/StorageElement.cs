using System;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Reports;

namespace Invest.TaxCalculator.BusinessLogic.Storage
{
    public class StorageElement
    {
        public Operation[] Operations { get; set; } = Array.Empty<Operation>();

        public Transactions.Transactions[] Transactions { get; set; } = Array.Empty<Transactions.Transactions>();

        public Report[] Reports { get; set; } = Array.Empty<Report>();

        public static StorageElement Empty => new();
    }
}