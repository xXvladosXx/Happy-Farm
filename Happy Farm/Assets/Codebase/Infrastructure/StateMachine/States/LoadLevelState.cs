using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Gameplay;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StateMachine.States.Core;
using Codebase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : ILoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;

        private string _saveName;
        private string _sceneName;

        private LevelStaticData _levelData;

        public LoadLevelState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            IGameFactory gameFactory,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }


        public async void Load(string save)
        {
            _sceneName = "Gameplay";

            await _sceneLoader.Load(_sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private async void OnLoaded()
        {
            _levelData = _staticDataService.ForLevel(_sceneName);

            // await _gameFactory.CreatePlayer(PlayerTypeID.Warrior, _levelData);
            await _gameFactory.CreatePlayer();
            await _gameFactory.CreateUI();
            
            foreach (var buildingSpawner in _levelData.BuildingSpawners)
            {
                await _gameFactory.CreateProductionSpawner(buildingSpawner.BuildingTypeID, buildingSpawner.Position);
            }
            
            Debug.Log("Loaded");
            _gameStateMachine.Enter<LoadProgressState, string>(_saveName);
        }


        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}