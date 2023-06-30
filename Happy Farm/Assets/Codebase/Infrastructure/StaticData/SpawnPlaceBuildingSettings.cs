using System.Collections.Generic;
using Codebase.Gameplay;
using Codebase.Logic.Entity.Building;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "SpawnPlaceBuildingSettings", menuName = "StaticData/SpawnPlaceBuildingSettings")]
    public abstract  class SpawnPlaceBuildingSettings : SerializedScriptableObject
    {
        public BuildingTypeID BuildingTypeID;
        public AssetReferenceGameObject SpawnPlacePrefab;
        public List<Upgrade> Upgrades;

        public abstract IBuildable CreateBuilding(IGameFactory gameFactory);
    }
    
    [CreateAssetMenu(fileName = "StorageSpawnPlaceBuildingSettings", menuName = "StaticData/StorageSpawnPlaceBuildingSettings")]
    public class StorageSpawnPlaceBuildingSettings : SpawnPlaceBuildingSettings
    {
        public override IBuildable CreateBuilding(IGameFactory gameFactory) => 
            new StorageConstructionBuildable(gameFactory);
    }
    
    [CreateAssetMenu(fileName = "ProductionSpawnPlaceBuildingSettings", menuName = "StaticData/ProductionSpawnPlaceBuildingSettings")]
    public class ProductionSpawnPlaceBuildingSettings : SpawnPlaceBuildingSettings
    {
        public override IBuildable CreateBuilding(IGameFactory gameFactory) => 
            new ProductionConstructionBuildable(gameFactory);
    }

}