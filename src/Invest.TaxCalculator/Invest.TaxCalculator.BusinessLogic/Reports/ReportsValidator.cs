using System.Linq;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Utils;

namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class ReportsValidator
    {
        private readonly ReportsRepository _repository = new();

        public void ValidateCreate(Report report)
        {
            ValidateFields(report);
            ValidateValidCreateYear(report);
        }

        public void ValidateDelete(Report report)
        {
            ValidateValidDeleteYear(report);
        }

        private void ValidateFields(Report report)
        {
            report.Year.Should().BePositive();
            report.Items.ForEach(ValidateFields);
        }

        private void ValidateFields(ReportItem reportItem)
        {
            if (reportItem.TaxPercent == 0)
            {
                reportItem.Profit.Should().Be(0);
                reportItem.TaxPercent.Should().Be(0);
                reportItem.Tax.Should().Be(0);
            }
            else
            {
                reportItem.TaxPercent.Should().BePositive();
                reportItem.Tax.Should().BeGreaterOrEqualTo(0);
            }
        }

        private void ValidateValidCreateYear(Report report)
        {
            var existYears = _repository
                .ReadAll()
                .Select(x => x.Year)
                .ToArray();

            if (!existYears.Any())
            {
                return;
            }

            existYears.Contains(report.Year).Should().BeFalse();
            existYears.Contains(report.Year - 1).Should().BeTrue();
        }

        private void ValidateValidDeleteYear(Report report)
        {
            var existYears = _repository
                .ReadAll()
                .Select(x => x.Year)
                .ToArray();

            existYears.Count(x => x > report.Year).Should().Be(0);
        }
    }
}