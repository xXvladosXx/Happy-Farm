using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.Stats;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Constructions
{
    public class FoodConstructionBuildable : IBuildable
    {
        private readonly IGameFactory _gameFactory;
        private IDestroyable _foodProduction;

        public FoodConstructionBuildable(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public bool IsSatisfied()
        {
            return true;
        }

        public async UniTask Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            _foodProduction?.Destroy();

            _foodProduction = await _gameFactory.CreateResourceProductionConstruction(buildingTypeID, parent.position, ResourceType.Food, ResourceType.Money);
        }
    }
}