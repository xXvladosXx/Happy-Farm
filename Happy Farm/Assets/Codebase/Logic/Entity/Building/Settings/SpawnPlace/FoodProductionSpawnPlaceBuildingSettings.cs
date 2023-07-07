using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.Building.Constructions;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Settings.SpawnPlace
{
    [CreateAssetMenu(fileName = "FoodProductionSpawnBuildingSettings", menuName = "StaticData/FoodProductionSpawnBuildingSettings", order = 0)]
    public class FoodProductionSpawnPlaceBuildingSettings : SpawnPlaceBuildingSettings
    {
        public override IBuildable CreateBuilding(IGameFactory gameFactory) => 
            new FoodConstructionBuildable(gameFactory);
    }
}