using Invest.TaxCalculator.BusinessLogic.Operations.Dto;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    public class CreateOperationHandler : IRequestHandler<CreateOperationCommand>
    {
        private readonly OperationsService _operationsService = new();
        private readonly Mapper _mapper = new();
        
        public Task<Unit> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var operation = _mapper.Map<OperationDto, Operation>(request.Operation);
            _operationsService.Create(operation);
            
            return Unit.Task;
        }
    }
    
    public class CreateOperationCommand : IRequest
    {
        public CreateOperationCommand(OperationDto operation)
        {
            Operation = operation;
        }

        public OperationDto Operation { get; set; }
    }
}