using System;
using Codebase.Logic.Entity.Movement;
using UnityEngine;

namespace Codebase.Logic.Animations.AnimationsReader
{
    public class AnimalStateReader : MonoBehaviour, IAnimatorStateReader
    {
        private Animator _animator;
        private IMovable _movable;
        public AnimatorStateHasher AnimatorStateHasher { get; set; }
        public AnimatorState State { get; private set; }
        public float DampTime { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void Construct(AnimatorStateHasher animatorStateHasher,
            Animator animator,
            IMovable movable,
            float dampTime)
        {
            _animator = animator;
            _movable = movable;
            DampTime = dampTime;
            AnimatorStateHasher = animatorStateHasher;
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        public void PlayAnimation(int hash, bool value)
        {
            _animator.SetBool(hash, value);
        }
        
        public void PlayAnimation(int hash)
        {
            _animator.Play("Interact");
        }

        public void Tick()
        {
            _animator.SetFloat(AnimatorStateHasher.SpeedHash, _movable.Speed, DampTime, Time.deltaTime);
        }
        
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == AnimatorStateHasher.IdleHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == AnimatorStateHasher.WalkHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == AnimatorStateHasher.InteractHash)
            {
                state = AnimatorState.Interact;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}