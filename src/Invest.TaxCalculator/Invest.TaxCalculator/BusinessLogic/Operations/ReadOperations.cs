using Invest.TaxCalculator.BusinessLogic.Operations.Dto;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    public class ReadOperationsHandler : IRequestHandler<ReadOperationsCommand, OperationsDto>
    {
        private readonly OperationsService _operationsService = new();
        private readonly Mapper _mapper = new();

        public Task<OperationsDto> Handle(ReadOperationsCommand request, CancellationToken cancellationToken)
        {
            var operationsArray = _operationsService
                .ReadAll()
                .Select(x => _mapper.Map<Operation, OperationDto>(x))
                .ToArray();

            var operationsByYear = operationsArray
                .GroupBy(x => x.DateTime.Year)
                .ToDictionary(
                    x => x.Key,
                    x => x.OrderBy(o => o.DateTime).ToArray()
                );

            var years = operationsByYear.Keys.OrderByDescending(x => x).ToArray();

            var operations = new OperationsDto
            {
                OperationsByYear = operationsByYear,
                Years = years,
            };

            return Task.FromResult(operations);
        }
    }
    
    public class ReadOperationsCommand : IRequest<OperationsDto>
    {
    }
}