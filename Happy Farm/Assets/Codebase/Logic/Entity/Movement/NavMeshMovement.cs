using UnityEngine;
using UnityEngine.AI;

namespace Codebase.Logic.Entity.Movement
{
    public class NavMeshMovement : IMovable
    {
        private readonly NavMeshAgent _navMeshAgent;

        public float Speed { get; private set; }
        public Vector3 Target => _navMeshAgent.destination;

        public NavMeshMovement(NavMeshAgent navMeshAgent,
            float speed)
        {
            _navMeshAgent = navMeshAgent;
            Speed = speed;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
            _navMeshAgent.speed = speed;
        }
        
        public void ResetSpeed()
        {
            if(!_navMeshAgent.isOnNavMesh)
                return;
            
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.ResetPath();
            _navMeshAgent.velocity = Vector3.zero;
        }

        public void Move(Vector3 target)
        {
            _navMeshAgent.updateRotation = true;
            _navMeshAgent.speed = Speed;
            _navMeshAgent.destination = target;
        }
    }
}