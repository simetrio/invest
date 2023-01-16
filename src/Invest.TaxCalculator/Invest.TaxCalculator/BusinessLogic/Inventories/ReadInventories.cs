using Invest.TaxCalculator.BusinessLogic.Inventories.Dto;
using Invest.TaxCalculator.BusinessLogic.Operations;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Inventories
{
    public class ReadInventoriesHandler : IRequestHandler<ReadInventoriesCommand, InventoryItemDto[]>
    {
        private readonly OperationsService _operationsService = new();
        private readonly Inventory _inventory = new();
        private readonly Mapper _mapper = new();

        public Task<InventoryItemDto[]> Handle(ReadInventoriesCommand request, CancellationToken cancellationToken)
        {
            var operations = _operationsService.ReadAll();

            var inventories = _inventory
                .Calculate(operations)
                .Select(x => _mapper.Map<InventoryItem, InventoryItemDto>(x))
                .OrderBy(x => x.Ticker)
                .ToArray();
            
            return Task.FromResult(inventories);
        }
    }
    
    public class ReadInventoriesCommand : IRequest<InventoryItemDto[]>
    {
    }
}