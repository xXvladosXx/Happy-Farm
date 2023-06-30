using Codebase.Gameplay;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States.Core
{
    public class GameOverState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public GameOverState(IGameStateMachine gameStateMachine,
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            Debug.Log("Congratulations! You won!");
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameOverState>
        {
        }
    }
}