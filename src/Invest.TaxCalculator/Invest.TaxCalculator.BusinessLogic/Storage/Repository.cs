using System;
using System.IO;
using Invest.TaxCalculator.BusinessLogic.Utils;

namespace Invest.TaxCalculator.BusinessLogic.Storage
{
    public class Repository
    {
        private static string? _fileName;
        private static StorageElement? _storageElement;

        private static readonly object Lock = new();

        // В приложении необходимо настроить имя файла
        public static string FileName { get; set; } = "TaxCalculatorDefault.txt";

        public void Update(StorageElement storageElement)
        {
            lock (Lock)
            {
                _storageElement = storageElement;
                File.WriteAllText(GetFileName(), storageElement.ToJson());
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
            return File.Exists(GetFileName())
                ? File.ReadAllText(GetFileName()).ToObject<StorageElement>()
                : StorageElement.Empty;
        }

        private static string GetFileName()
        {
            return _fileName ??= CalculateFileName();
        }

        private static string CalculateFileName()
        {
            var directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "invest"
            );

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return Path.Combine(directory, FileName);
        }
    }
}