using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Exit()
        {
        }

        public void Enter()
        {
            if(_sceneLoader.GetCurrentScene != "StartScene")
                _sceneLoader.Load("StartScene");
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
        {
        }
    }
}