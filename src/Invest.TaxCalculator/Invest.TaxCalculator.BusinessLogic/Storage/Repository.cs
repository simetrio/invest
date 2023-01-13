using System.IO;
using Invest.TaxCalculator.BusinessLogic.Utils;

namespace Invest.TaxCalculator.BusinessLogic.Storage
{
    public class Repository
    {
        private const string FileName = "TaxCalculator.txt";

        private static StorageElement? _storageElement;

        private static readonly object Lock = new();

        public void Update(StorageElement storageElement)
        {
            lock (Lock)
            {
                _storageElement = storageElement;
                File.WriteAllText(FileName, storageElement.ToJson());
            }
        }

        public StorageElement ReadAll()
        {
            lock (Lock)
            {
                return _storageElement ??= Read();
            }
        }

        private static StorageElement Read()
        {
            return File.Exists(FileName)
                ? File.ReadAllText(FileName).ToObject<StorageElement>()
                : StorageElement.Empty;
        }
    }
}