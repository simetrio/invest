using Invest.TaxCalculator.BusinessLogic.Transactions;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class DeleteReportHandler : IRequestHandler<DeleteReportCommand>
    {
        private readonly TransactionsService _transactionsService = new();
        private readonly ReportsService _reportService = new();

        public Task<Unit> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var report = _reportService.ReadAll().Single(x => x.Year == request.Year);
            var transactions = _transactionsService.ReadAll().Single(x => x.Year == request.Year);
            
            _reportService.Delete(report);
            _transactionsService.Delete(transactions);

            return Unit.Task;
        }
    }

    public class DeleteReportCommand : IRequest
    {
        public DeleteReportCommand(int year)
        {
            Year = year;
        }

        public int Year { get; }
    }
}