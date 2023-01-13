using System;

namespace Invest.TaxCalculator.BusinessLogic.Storage
{
    public abstract class EntityRepository<T>
    {
        private readonly Repository _repository = new();

        protected abstract void CreateOrUpdate(StorageElement storageElement, T[] items);

        protected abstract T[] ReadAll(StorageElement storageElement);

        protected abstract void Delete(StorageElement storageElement, T[] items);

        public void CreateOrUpdate(T[] items)
        {
            Update(CreateOrUpdate, items);
        }

        public T[] ReadAll()
        {
            var storageElement = _repository.ReadAll();
            return ReadAll(storageElement);
        }

        public void Delete(T[] items)
        {
            Update(Delete, items);
        }

        private void Update(Action<StorageElement, T[]> update, T[] items)
        {
            var storageElement = _repository.ReadAll();
            update(storageElement, items);
            _repository.Update(storageElement);
        }
    }
}