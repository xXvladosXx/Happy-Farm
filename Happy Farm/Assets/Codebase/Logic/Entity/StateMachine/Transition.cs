using System;

namespace Codebase.Logic.Entity.StateMachine
{
    public class Transition<TInitializer>
    {
        public Func<bool> Condition { get; }
        public State<TInitializer> From { get; }
        public State<TInitializer> To { get; }

        public Transition(State<TInitializer> from, State<TInitializer> to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}