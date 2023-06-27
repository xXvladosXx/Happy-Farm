using System;
using Codebase.Logic.Animations;
using Codebase.Logic.Animations.AnimationsReader;
using Codebase.Logic.Entity.EnemyEntities.Catch;
using Codebase.Logic.Entity.EnemyEntities.States;
using Codebase.Logic.Entity.Movement;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using Codebase.Utils.Transform;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities
{
    public class EnemyAnimal
    {
        public readonly ICatchable Catchable;
        public readonly ICollectable Collectable;
        public readonly IMovable Movement;
        public readonly IAnimatorStateReader AnimatorStateReader;
        public Transform Transform { get; private set; }

        private readonly EntityStateMachine<EnemyAnimal> _stateMachine;

        public EnemyAnimal(Transform transform, 
            IMovable movement,
            ICatchable catchable,
            ICollectable collectable,
            IAnimatorStateReader animatorStateReader)
        {
            Movement = movement;
            Transform = transform;
            Catchable = catchable;
            Collectable = collectable;
            AnimatorStateReader = animatorStateReader;

            _stateMachine = new EntityStateMachine<EnemyAnimal>();

            var idleState = new EnemyAnimalIdleState(this);
            var walkState = new EnemyAnimalWalkState(this);
            var caughtState = new EnemyAnimalCaughtState(this);
            var runState = new EnemyAnimalRunState(this);
            var duringCatchState = new EnemyAnimalDuringCatchState(this);

            _stateMachine.AddStates(caughtState, idleState, walkState, runState, duringCatchState);
        }
        
        public void BindTransitions()
        {
            Func<bool> isCaught = () => Catchable.IsCaught;
            Func<bool> duringCatch = () => Catchable.CurrentAmountToCatch > 0;
            Func<bool> isWaitingCaught = () => Catchable.CurrentTimeToWaitCaught > Catchable.MaxTimeToWaitCaught;
            Func<bool> isNearTarget = () => Transform.IsNear(Movement.Target, 2);
            Func<bool> hasTarget = () => Movement.Target != Vector3.zero;

            _stateMachine.AddTransition<EnemyAnimalIdleState, EnemyAnimalWalkState>(hasTarget);
            _stateMachine.AddTransition<EnemyAnimalWalkState, EnemyAnimalIdleState>(isNearTarget);
            
            _stateMachine.AddTransition<EnemyAnimalWalkState, EnemyAnimalDuringCatchState>(duringCatch);
            _stateMachine.AddTransition<EnemyAnimalDuringCatchState, EnemyAnimalWalkState>(() => !duringCatch());
            
            _stateMachine.AddTransition<EnemyAnimalDuringCatchState, EnemyAnimalCaughtState>(isCaught);
            _stateMachine.AddTransition<EnemyAnimalCaughtState, EnemyAnimalRunState>(isWaitingCaught);
            
            _stateMachine.SetState<EnemyAnimalIdleState>();
        }
        
        public void Update() => 
            _stateMachine.Update();
    }
}