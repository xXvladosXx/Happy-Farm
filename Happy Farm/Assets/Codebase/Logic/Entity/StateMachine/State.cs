namespace Codebase.Logic.Entity.StateMachine
{
    public abstract class State<TInitializer>
    {
        public TInitializer Initializer { get; }
        public bool IsActive { get; set; }

        protected State(TInitializer stateInitializer)
        {
            Initializer = stateInitializer;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnExit() { }
    }
}