using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Operations
{
    public class DeleteOperationHandler : IRequestHandler<DeleteOperationCommand>
    {
        private readonly OperationsService _operationsService = new();

        public Task<Unit> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
        {
            var operation = _operationsService.ReadAll().Single(x => x.Id == request.Id);
            _operationsService.Delete(operation);

            return Unit.Task;
        }
    }

    public class DeleteOperationCommand : IRequest
    {
        public DeleteOperationCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}