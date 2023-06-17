using Codebase.Logic.Entity.StateMachine;
using Codebase.Utils.Transform;
using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalWalkState : State<ProductionAnimal>
    {

        public ProductionAnimalWalkState(ProductionAnimal stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.Movement.SetSpeed(2);

            var target = GetRandomNavMeshPoint(Initializer.Transform.position, 15);
            Initializer.Movement.Move(target);
        }

        public override void OnUpdate()
        {
            Initializer.Eater.Starve();
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