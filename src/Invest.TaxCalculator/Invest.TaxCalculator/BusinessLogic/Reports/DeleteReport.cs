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
            var report = _reportService.ReadAll().SingleOrDefault(x => x.Year == request.Year);
            var transactions = _transactionsService.ReadAll().SingleOrDefault(x => x.Year == request.Year);
            
            if(report != null) _reportService.Delete(report);
            if(transactions != null) _transactionsService.Delete(transactions);

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