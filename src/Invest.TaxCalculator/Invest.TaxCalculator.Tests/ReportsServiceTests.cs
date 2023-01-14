using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Invest.TaxCalculator.BusinessLogic.Reports;
using Invest.TaxCalculator.BusinessLogic.Storage;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.Tests.Utils;
using NUnit.Framework;

namespace Invest.TaxCalculator.Tests
{
    public class ReportsServiceTests
    {
        private readonly ReportsService _reportsService = new();
        private readonly Repository _repository = new();

        private static readonly IFixture Fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _repository.Update(StorageElement.Empty);
        }

        [TestCaseSource(nameof(ValidateFieldsData))]
        public void ValidateFields(Report report, string field)
        {
            Action action = () => _reportsService.Create(report);
            Func<Report[]> actual = () => _reportsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message.Contains($".{field} "));

            actual().Should().BeEmpty();
        }

        [Test]
        public void ValidateNotExistsYear()
        {
            var builder = new EntityBuilder()
                .WithReportItem();

            var report = new Report
            {
                Year = 2019,
                Items = builder.ReportItems,
            };

            _reportsService.Create(report);

            Action action = () => _reportsService.Create(report);
            Func<Report[]> actual = () => _reportsService.ReadAll();

            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Contains(report.Year) to be false, but found True"));

            actual().Should().BeEquivalentTo(new[] {report});
        }

        [Test]
        public void ValidateExistsPreviewYear()
        {
            var builder = new EntityBuilder()
                .WithReportItem();

            var report1 = new Report
            {
                Year = 2019,
                Items = builder.ReportItems,
            };

            var report2 = new Report
            {
                Year = 2021,
                Items = builder.ReportItems,
            };
        
            _reportsService.Create(report1);
        
            Action action = () => _reportsService.Create(report2);
            Func<Report[]> actual = () => _reportsService.ReadAll();
        
            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Contains(report.Year - 1) to be true, but found False"));
        
            actual().Should().BeEquivalentTo(new[] {report1});
        }
        
        [Test]
        public void ValidateNotDeleteWhenHasFutureReports()
        {
            var builder = new EntityBuilder()
                .WithReportItem();

            var report1 = new Report
            {
                Year = 2019,
                Items = builder.ReportItems,
            };

            var report2 = new Report
            {
                Year = 2020,
                Items = builder.ReportItems,
            };
        
            _reportsService.Create(report1);
            _reportsService.Create(report2);
        
            Action action = () => _reportsService.Delete(report1);
            Func<Report[]> actual = () => _reportsService.ReadAll();
        
            action
                .Should()
                .Throw<AssertionException>()
                .Where(x => x.Message
                    .Contains("Expected existYears.Count(x => x > report.Year) to be 0, but found 1")
                );
        
            actual().Should().BeEquivalentTo(new[] {report1, report2});
        }
        
        [Test]
        public void Delete()
        {
            var builder = new EntityBuilder()
                .WithReportItem();

            var report = new Report
            {
                Year = 2019,
                Items = builder.ReportItems,
            };
        
            _reportsService.Create(report);
            _reportsService.Delete(report);
        
            var actual = _reportsService.ReadAll();
        
            actual.Should().BeEmpty();
        }

        private static IEnumerable<TestCaseData> ValidateFieldsData()
        {
            yield return CreateReportItem(
                x => { x.Profit = Fixture.Create<decimal>(); },
                nameof(ReportItem.Profit)
            ).SetName(nameof(ReportItem.Profit));

            yield return CreateReportItem(
                x => { x.Tax = Fixture.Create<decimal>(); },
                nameof(ReportItem.Tax)
            ).SetName(nameof(ReportItem.Tax));

            yield return CreateReportItem(
                x => { x.TaxPercent = Fixture.Create<decimal>(); },
                nameof(ReportItem.Profit)
            ).SetName(nameof(ReportItem.Profit));

            yield return CreateReportItem(
                x =>
                {
                    x.Profit = Fixture.Create<decimal>();
                    x.TaxPercent = Fixture.Create<decimal>();
                },
                nameof(ReportItem.Tax)
            ).SetName(nameof(ReportItem.Tax));

            yield return CreateReport(
                x => { },
                nameof(Transactions.Year)
            ).SetName(nameof(Transactions.Year));
        }

        private static TestCaseData CreateReport(Action<Report> update, string filed)
        {
            var report = new Report();
            update(report);

            return new TestCaseData(report, filed);
        }

        private static TestCaseData CreateReportItem(Action<ReportItem> update, string filed)
        {
            var reportItem = new ReportItem();
            update(reportItem);

            var transactions = new Report
            {
                Year = 2019,
                Items = new[] {reportItem}
            };

            return new TestCaseData(transactions, filed);
        }
    }
}