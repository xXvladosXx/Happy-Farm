using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.Building.Constructions;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Settings.SpawnPlace
{
    [CreateAssetMenu(fileName = "ProductionSpawnPlaceBuildingSettings", menuName = "StaticData/ProductionSpawnPlaceBuildingSettings")]
    public class ProductionSpawnPlaceBuildingSettings : SpawnPlaceBuildingSettings
    {
        public override IBuildable CreateBuilding(IGameFactory gameFactory) => 
            new ProductionConstructionBuildable(gameFactory);
    }
}