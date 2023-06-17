using System;
using UnityEngine;

namespace Codebase.Logic.Animations
{
    public class AnimalStateReader : MonoBehaviour, IAnimatorStateReader
    {
        private Animator _animator;
        private Rigidbody _rigidbody;
        public AnimatorStateHasher AnimatorStateHasher { get; set; }
        public AnimatorState State { get; private set; }
        public float DampTime { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void Construct(AnimatorStateHasher animatorStateHasher,
            Animator animator,
            Rigidbody rigidbody,
            float dampTime)
        {
            _animator = animator;
            _rigidbody = rigidbody;
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
            _animator.SetFloat(AnimatorStateHasher.SpeedHash, _rigidbody.velocity.magnitude, DampTime, Time.deltaTime);
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