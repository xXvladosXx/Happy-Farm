using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Codebase.Logic.Stats;
using Codebase.Logic.Storage;
using Codebase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        UniTask CreatePlayer();
        UniTask<ProductionAnimal> CreateProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID);
        UniTask<EnemyAnimal> CreateEnemyAnimal(EnemyAnimalTypeID productionAnimalTypeID, Vector3 position);
        UniTask<Eatable> CreateFood(string foodName, Vector3 position);
        UniTask<IDestroyable> CreateStorage(BuildingTypeID buildingTypeID, Vector3 position);
        UniTask<GameObject> CreateProduct(string productId, Vector3 position, int productionAmount);
        UniTask<IDestroyable> CreateProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position);
        UniTask<IDestroyable> CreateResourceProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position,
            ResourceType productionResourceType, ResourceType consumptionResourceType);
        UniTask CreateProductionSpawner(BuildingTypeID buildingTypeID, Vector3 buildingSpawnerPosition);
        UniTask CreateUI();
        void CreateResources(List<ResourcesData> levelDataResources);
        void CreateEnemySpawner(EnemyAnimalTypeID enemyAnimalTypeID, Vector3 spawnPosition, float time, bool isLooped,
            PortalParticleData enemySpawnerPortalParticleData);
    }
}