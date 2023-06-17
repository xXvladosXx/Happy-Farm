using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities.States
{
    public class EnemyAnimalDuringCatchState : State<EnemyAnimal>
    {
        private float _startSpeed;
        public EnemyAnimalDuringCatchState(EnemyAnimal stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            _startSpeed = Initializer.Movement.Speed;
            
            var newSpeed = Initializer.Movement.Speed - Initializer.Movement.Speed * 0.5f;
            Initializer.Movement.SetSpeed(newSpeed); 
        }

        public override void OnUpdate()
        {
            Initializer.AnimatorStateReader.Tick();
            
            // var newSpeed = Initializer.Movement.Speed - Initializer.Catchable.CurrentAmountToCatch;
            // Initializer.Movement.SetSpeed(newSpeed); 
        }

        public override void OnExit()
        {
            Initializer.Movement.SetSpeed(_startSpeed); 
        }
    }
}