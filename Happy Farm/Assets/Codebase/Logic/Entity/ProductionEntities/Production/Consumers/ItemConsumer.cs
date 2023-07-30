using Codebase.Logic.Storage;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Consumers
{
    public class ItemConsumer : IConsumer<string, int>
    {
        public int MaxAmount { get; private set; }
        private readonly IStorageUser _storageUser;
        private readonly string _product;

        public ItemConsumer(IStorageUser storageUser,
            string product,
            int maxAmount)
        {
            MaxAmount = maxAmount;
            _storageUser = storageUser;
            _product = product;
        }

        public string GetProduct() => _product;
        public bool CanConsume(int amount)
        {
            var itemAmount = _storageUser.Inventory.FindItemAmount(_product);
            return itemAmount > 0;
        }

        public int Consume(int amount)
        {
            var itemAmount = _storageUser.Inventory.FindItemAmount(_product);
            var clamp = Mathf.Clamp(itemAmount, 0, amount);

            _storageUser.Inventory.RemoveItem(_product, clamp);

            return clamp;
        }
    }
}