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
        private ProductionConstruction _foodProduction;

        public FoodConstructionBuildable(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public bool IsSatisfied()
        {
            if(_foodProduction == null)
                return true;
            
            return !_foodProduction.Producer.InProduction;
        }

        public async UniTask Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            _foodProduction?.Recycle();

            _foodProduction = await _gameFactory.CreateResourceProductionConstruction(buildingTypeID, parent.position, ResourceType.Food, ResourceType.Money);
        }
    }
}