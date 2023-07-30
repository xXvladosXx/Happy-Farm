using System;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Zenject;

namespace Codebase.Logic.ShopSystem
{
    public class Shop : IShop, IInitializable, IDisposable
    {
        private readonly IResourcesStorage _resourcesStorage;

        public Shop(IResourcesStorage resourcesStorage)
        {
            _resourcesStorage = resourcesStorage;
        }

        public bool WasSold(int amount)
        {
            _resourcesStorage.Add(ResourceType.Money, amount);
            return true;
        }

        public void Buy(int amount)
        {
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}