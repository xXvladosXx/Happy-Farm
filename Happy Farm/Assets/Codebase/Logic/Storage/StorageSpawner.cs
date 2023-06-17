using System;
using Codebase.Gameplay;
using Sirenix.OdinInspector;
using Zenject;

namespace Codebase.Logic.Storage
{
    public class StorageSpawner : SerializedMonoBehaviour
    {
        private GameFactory _gameFactory;

        [Inject]
        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        private void Awake()
        {
            _gameFactory.CreatePlayerStorage();
        }
    }
}