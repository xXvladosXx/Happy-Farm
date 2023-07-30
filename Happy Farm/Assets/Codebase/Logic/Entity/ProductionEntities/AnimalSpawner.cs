using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Codebase.Logic.ShopSystem;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities
{
    public class AnimalSpawner : IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IResourcesStorage _resourcesStorage;
        private readonly IStaticDataService _staticDataService;

        public AnimalSpawner(IGameFactory gameFactory, 
            IResourcesStorage resourcesStorage,
            IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _resourcesStorage = resourcesStorage;
            _staticDataService = staticDataService;
        }
        
        public void Initialize()
        {
            
        }

        public void CreateProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID)
        {
            if (_resourcesStorage.HasResource(ResourceType.Money, _staticDataService.GetProductionAnimal(productionAnimalTypeID).Price))
            {
                _gameFactory.CreateProductionAnimal(productionAnimalTypeID);
                _resourcesStorage.Remove(ResourceType.Money, _staticDataService.GetProductionAnimal(productionAnimalTypeID).Price);
            }
        }
    }
}