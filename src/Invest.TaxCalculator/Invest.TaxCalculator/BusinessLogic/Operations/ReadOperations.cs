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
                .OrderByDescending(x => x.DateTime)
                .Select(x => _mapper.Map<Operation, OperationDto>(x))
                .ToArray();

            var operations = new OperationsDto
            {
                Items = operationsArray,
            };

            return Task.FromResult(operations);
        }
    }
    
    public class ReadOperationsCommand : IRequest<OperationsDto>
    {
    }
}