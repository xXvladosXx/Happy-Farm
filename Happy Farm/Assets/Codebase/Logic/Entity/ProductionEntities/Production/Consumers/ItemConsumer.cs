using Codebase.Logic.Storage;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Consumers
{
    public class ItemConsumer : IConsumer<string>
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

        public int Consume()
        {
            var itemAmount = _storageUser.Inventory.FindItemAmount(_product);
            var clamp = Mathf.Clamp(itemAmount, 0, MaxAmount);

            _storageUser.Inventory.RemoveItem(_product, clamp);

            return clamp;
        }
    }
}