using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity;
using Codebase.UI;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States.Core
{
    public class GameOverState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly PersistentUI _persistentUI;
        private readonly GameBehaviourHandler _gameBehaviourHandler;

        public GameOverState(IGameStateMachine gameStateMachine,
            PersistentUI persistentUI,
            GameBehaviourHandler gameBehaviourHandler)
        {
            _gameStateMachine = gameStateMachine;
            _persistentUI = persistentUI;
            _gameBehaviourHandler = gameBehaviourHandler;
        }
        
        public void Enter()
        {
            _persistentUI.GameOverUI.RestartButton.onClick.AddListener(RestartLevel);
            Debug.Log("Congratulations! You won!");
        }

        private void RestartLevel()
        {
            _gameBehaviourHandler.Clear();
            _gameStateMachine.Enter<LoadLevelState, string>("Gameplay");
            _persistentUI.GameOverUI.RestartButton.onClick.RemoveListener(RestartLevel);
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameOverState>
        {
        }
    }
}