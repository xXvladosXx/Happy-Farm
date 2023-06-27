using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Storage.Container;
using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Logic.Entity.EnemyEntities.States
{
    public class EnemyAnimalWalkState : State<EnemyAnimal>
    {
        public EnemyAnimalWalkState(EnemyAnimal stateInitializer) : base(stateInitializer)
        {
        }
        
        public override void OnEnter()
        {
            Initializer.Collectable.CanBeCollected = false;

            Initializer.Movement.SetSpeed(Initializer.Movement.IdleSpeed);

            var target = GetRandomNavMeshPoint(Initializer.Transform.position, 15);
            Initializer.Movement.Move(target);
        }

        public override void OnUpdate()
        {
            Initializer.AnimatorStateReader.Tick();
        }
        
        private Vector3 GetRandomNavMeshPoint(Vector3 origin, float distance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;

            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas);

            return navMeshHit.position;
        }
    }
}