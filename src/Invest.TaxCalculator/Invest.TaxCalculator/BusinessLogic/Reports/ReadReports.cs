using Invest.TaxCalculator.BusinessLogic.Reports.Dto;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Reports
{
    public class ReadReportsHandler : IRequestHandler<ReadReportsCommand, ReportDto[]>
    {
        private readonly ReportsService _reportsService = new();
        private readonly Mapper _mapper = new();

        public Task<ReportDto[]> Handle(ReadReportsCommand request, CancellationToken cancellationToken)
        {
            var reports = _reportsService
                .ReadAll()
                .OrderByDescending(x => x.Year)
                .Select(x => _mapper.Map<Report, ReportDto>(x))
                .ToArray();
            
            return Task.FromResult(reports);
        }
    }
    
    public class ReadReportsCommand : IRequest<ReportDto[]>
    {
    }
}