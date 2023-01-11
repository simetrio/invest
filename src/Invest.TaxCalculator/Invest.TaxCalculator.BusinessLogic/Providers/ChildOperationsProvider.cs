using System.Collections.Generic;
using System.Linq;
using Invest.TaxCalculator.BusinessLogic.Operations;

namespace Invest.TaxCalculator.BusinessLogic.Providers
{
    /// <summary>
    ///     Провайдер дочерних транзакций
    /// </summary>
    public class ChildOperationsProvider
    {
        private readonly ILookup<string, Operation> _operationsByParentId;

        public ChildOperationsProvider(OperationsCollection operations)
        {
            _operationsByParentId = operations
                .All
                .Where(x => x.ParentId != null)
                .ToLookup(x => x.ParentId!);
        }

        public IEnumerable<Operation> Get(string id)
        {
            return _operationsByParentId[id];
        }
    }
}