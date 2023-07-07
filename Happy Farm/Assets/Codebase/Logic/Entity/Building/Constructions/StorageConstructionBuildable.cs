using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Constructions
{
    public class StorageConstructionBuildable : IBuildable
    {
        private readonly IGameFactory _gameFactory;
        private Storage.Storage _storage;

        public StorageConstructionBuildable(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public bool IsSatisfied()
        {
            return true;
        }

        public async UniTask Build(BuildingTypeID buildingTypeID, Transform parent)
        {
            if(_storage != null)
                Object.Destroy(_storage.transform.gameObject);
            
            _storage = await _gameFactory.CreateStorage(buildingTypeID, parent.position);
        }
    }
}