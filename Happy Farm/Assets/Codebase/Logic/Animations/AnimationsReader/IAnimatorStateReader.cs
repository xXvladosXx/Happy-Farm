using System;

namespace Codebase.Logic.Animations
{
    public interface IAnimatorStateReader
    {
        AnimatorStateHasher AnimatorStateHasher { get; }
        AnimatorState State { get; }
        float DampTime { get; }
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        void PlayAnimation(int hash, bool value);
        void Tick();
        void PlayAnimation(int hash);
    }
}