using Codebase.Infrastructure.Factory;
using Codebase.Logic.Stats;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Constructions
{
    public class ProductionConstructionBuildable : IBuildable
    {
        private IDestroyable _currentBuilding;
        private readonly IGameFactory _gameFactory;

        public ProductionConstructionBuildable(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public bool IsSatisfied()
        {
            if(_currentBuilding == null)
                return true;

            return true;
        }

        public async UniTask Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            _currentBuilding?.Destroy();

            _currentBuilding =
                await _gameFactory.CreateProductionConstruction(buildingTypeID, parent.position);
        }
    }
}