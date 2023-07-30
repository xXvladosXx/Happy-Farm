using System;
using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.UI;
using Zenject;

namespace Codebase.Controllers
{
    public class AnimalSpawnerController : IInitializable, IDisposable
    {
        private readonly AnimalSpawner _animalSpawner;
        private readonly GameplayUI _gameplayUI;

        public AnimalSpawnerController(AnimalSpawner animalSpawner,
            GameplayUI gameplayUI)
        {
            _animalSpawner = animalSpawner;
            _gameplayUI = gameplayUI;
        }

        public void Initialize()
        {
            _gameplayUI.SpawnerUI.Initialize(_animalSpawner);
        }

        public void Dispose()
        {
            _gameplayUI.SpawnerUI.Dispose();
        }
    }
}