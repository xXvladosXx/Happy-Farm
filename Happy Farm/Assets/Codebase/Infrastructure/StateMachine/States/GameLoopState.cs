using Codebase.Gameplay;
using Codebase.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly GameFactory _gameFactory;

        public GameLoopState(IGameStateMachine gameStateMachine,
            GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
          
        }
   
        private void OnPausePerformed()
        {
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}