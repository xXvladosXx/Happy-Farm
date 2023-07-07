using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Constructions
{
    public class FoodConstructionBuildable : IBuildable
    {
        private readonly IGameFactory _gameFactory;
        private Transform _foodProduction;

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
            if(_foodProduction != null)
                Object.Destroy(_foodProduction.gameObject);
            
            _foodProduction = await _gameFactory.CreateFoodProductionConstruction(buildingTypeID, parent.position);
        }
    }
}