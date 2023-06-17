using System;
using System.Collections.Generic;
using Codebase.Infrastructure.StateMachine.States;
using Codebase.Infrastructure.StateMachine.States.Core;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(BootstrapState.Factory bootstrapStateFactory,
            LoadLevelState.Factory loadLevelStateFactory,
            LoadProgressState.Factory loadProgressStateFactory,
            GameLoopState.Factory gameLoopStateFactory,
            GamePausedState.Factory gamePausedStateFactory)
        {
            _states = new Dictionary<Type, IExitableState>();
            
            RegisterState(bootstrapStateFactory.Create(this));
            RegisterState(loadLevelStateFactory.Create(this));
            RegisterState(loadProgressStateFactory.Create(this));
            RegisterState(gameLoopStateFactory.Create(this));
            RegisterState(gamePausedStateFactory.Create(this));
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TLoad>(TLoad load) where TState : class, ILoadState<TLoad>
        {
            var newState = ChangeState<TState>();
            newState.Load(load);
        }
        
        private void RegisterState<TState>(TState state) where TState : class, IExitableState
            => _states.Add(typeof(TState), state);
        
        private void UnregisterState<TState>() where TState : class, IExitableState
            => _states.Remove(typeof(TState));
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            var state = _states[typeof(TState)] as TState;
            _currentState = state;

            return state;
        }
        
        
    }
}