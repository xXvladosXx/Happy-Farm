using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public GameLoopState(IGameStateMachine gameStateMachine,
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
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