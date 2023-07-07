using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.Building.Constructions;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Settings.SpawnPlace
{
    [CreateAssetMenu(fileName = "StorageSpawnPlaceBuildingSettings", menuName = "StaticData/StorageSpawnPlaceBuildingSettings")]
    public class StorageSpawnPlaceBuildingSettings : SpawnPlaceBuildingSettings
    {
        public override IBuildable CreateBuilding(IGameFactory gameFactory) => 
            new StorageConstructionBuildable(gameFactory);
    }
}