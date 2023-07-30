using Codebase.Infrastructure.Factory;
using Codebase.Logic.Stats;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.Constructions
{
    public class StorageConstructionBuildable : IBuildable
    {
        private readonly IGameFactory _gameFactory;
        private IDestroyable _storage;

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
            _storage?.Destroy();

            _storage = await _gameFactory.CreateStorage(buildingTypeID, parent.position);
        }
    }
}