using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Gameplay;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StateMachine.States.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : ILoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly GameFactory _gameFactory;

        private string _saveName;
        private string _sceneName;

        public LoadLevelState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader, 
            GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
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
            // _levelData = _staticDataService.ForLevel(_sceneName);

           // await _gameFactory.CreatePlayer(PlayerTypeID.Warrior, _levelData);
            Debug.Log("Loaded");
            _gameStateMachine.Enter<LoadProgressState, string>(_saveName);
        }


        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}