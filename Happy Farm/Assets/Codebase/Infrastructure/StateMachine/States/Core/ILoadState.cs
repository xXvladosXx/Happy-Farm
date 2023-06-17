namespace Codebase.Infrastructure.StateMachine.States.Core
{
    public interface ILoadState<in TLoad> : IExitableState
    {
        void Load(TLoad save);
    }
}