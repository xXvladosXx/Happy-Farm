namespace Codebase.Infrastructure.StateMachine.States.Core
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}