using Codebase.Infrastructure.StateMachine.States.Core;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States
{
    public class LoadProgressState : ILoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LoadProgressState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public async void Load(string save)
        {
            _gameStateMachine.Enter<GamePausedState>();
            await UniTask.Delay(400);
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadProgressState>
        {
        }
    }
}