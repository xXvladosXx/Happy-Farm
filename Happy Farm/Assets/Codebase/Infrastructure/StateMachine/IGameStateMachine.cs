using Codebase.Infrastructure.StateMachine.States.Core;

namespace Codebase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TLoad>(TLoad level) where TState : class, ILoadState<TLoad>;
    }
}