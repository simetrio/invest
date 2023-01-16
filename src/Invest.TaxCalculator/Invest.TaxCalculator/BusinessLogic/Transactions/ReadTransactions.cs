using Invest.TaxCalculator.BusinessLogic.Transactions.Dto;
using MediatR;

namespace Invest.TaxCalculator.BusinessLogic.Transactions
{
    public class ReadTransactionsHandler : IRequestHandler<ReadTransactionsCommand, TransactionsDto[]>
    {
        private readonly TransactionsService _transactionsService = new();
        private readonly Mapper _mapper = new();

        public Task<TransactionsDto[]> Handle(ReadTransactionsCommand request, CancellationToken cancellationToken)
        {
            var transactions = _transactionsService
                .ReadAll()
                .OrderByDescending(x => x.Year)
                .Select(x => _mapper.Map<Transactions, TransactionsDto>(x))
                .ToArray();
            
            return Task.FromResult(transactions);
        }
    }
    
    public class ReadTransactionsCommand : IRequest<TransactionsDto[]>
    {
    }
}