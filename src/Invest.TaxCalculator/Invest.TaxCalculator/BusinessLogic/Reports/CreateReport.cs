using Invest.TaxCalculator.BusinessLogic.Calculate;
using Invest.TaxCalculator.BusinessLogic.Operations;
using Invest.TaxCalculator.BusinessLogic.Transactions;
using Invest.TaxCalculator.BusinessLogic.Transactions.Calculator;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class CreateReportHandler : IRequestHandler<CreateReportCommand>
    {
        private readonly Mapper _mapper = new();
        private readonly OperationsService _operationsService = new();
        private readonly TransactionsService _transactionsService = new();
        private readonly ReportsService _reportsService = new();
        private readonly TransactionsCalculator _transactionsCalculator = new();
        private readonly Calculator _calculator = new();

        public Task<Unit> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var year = request.Year;
            var operations = _operationsService.ReadAll();
            var oldTransactions = _transactionsService.ReadAll()
                .SelectMany(x => x.Items)
                .ToArray();

            var newTransactions = _transactionsCalculator.Calculate(operations, oldTransactions, year);
            var report = _calculator.Calculate(newTransactions.Items, year);
            
            _transactionsService.Create(newTransactions);
            _reportsService.Create(report);

            return Unit.Task;
        }
    }

    public class CreateReportCommand : IRequest
    {
        public CreateReportCommand(int year)
        {
            Year = year;
        }

        public int Year { get; }
    }
}