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
        private readonly Dictionary<string, Operation> _operationsByParentId;

        public ChildOperationsProvider(Operation[] operations)
        {
            _operationsByParentId = operations
                .Where(x => x.ParentId != null)
                .ToDictionary(x => x.ParentId!);
        }

        public Operation? TryGet(string id)
        {
            return _operationsByParentId.TryGetValue(id, out var operation) ? operation : null;
        }
    }
}