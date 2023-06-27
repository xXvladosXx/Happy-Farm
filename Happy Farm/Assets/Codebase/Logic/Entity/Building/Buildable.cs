using Codebase.Gameplay;
using Codebase.Infrastructure.StaticData;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public interface IBuildable
    {
        bool IsSatisfied();
        void Build(BuildingTypeID buildingTypeID, Transform parent);
    }
    
    public class ProductionConstructionBuildable : IBuildable
    {
        private ProductionConstruction _currentBuilding;
        private readonly GameFactory _gameFactory;

        public ProductionConstructionBuildable(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public bool IsSatisfied()
        {
            if(_currentBuilding == null)
                return true;
            
            return !_currentBuilding.Producer.InProduction;
        }

        public async void Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            if(_currentBuilding != null)
                Object.Destroy(_currentBuilding.Transform.gameObject);
            
            if(_currentBuilding != null && _currentBuilding.Producer.InProduction)
                return;
            
            _currentBuilding =
                await _gameFactory.CreateProductionConstruction(buildingTypeID, parent.position);
        }
    }
    
    public class StorageConstructionBuildable : IBuildable
    {
        private readonly GameFactory _gameFactory;
        private Storage.Storage _storage;

        public StorageConstructionBuildable(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public bool IsSatisfied()
        {
            return true;
        }

        public async void Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            if(_storage != null)
                Object.Destroy(_storage.transform.gameObject);
            
            _storage = await _gameFactory.CreateStorage(buildingTypeID, parent.position);
        }
    }
}