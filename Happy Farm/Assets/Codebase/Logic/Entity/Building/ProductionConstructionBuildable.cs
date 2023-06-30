using Codebase.Gameplay;
using Codebase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class ProductionConstructionBuildable : IBuildable
    {
        private ProductionConstruction _currentBuilding;
        private readonly IGameFactory _gameFactory;

        public ProductionConstructionBuildable(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public bool IsSatisfied()
        {
            if(_currentBuilding == null)
                return true;
            
            return !_currentBuilding.Producer.InProduction;
        }

        public async UniTask Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            if(_currentBuilding != null)
                Object.Destroy(_currentBuilding.Transform.gameObject);
            
            if(_currentBuilding != null && _currentBuilding.Producer.InProduction)
                return;
            
            _currentBuilding =
                await _gameFactory.CreateProductionConstruction(buildingTypeID, parent.position);
        }
    }
}