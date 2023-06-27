using UnityEngine;

namespace Codebase.Logic.Entity.Movement
{
    public interface IMovable
    {
        float IdleSpeed { get; }
        float RunSpeed { get; }
        Vector3 Target { get; }
        float Speed { get; }
        void ResetSpeed();
        void Move(Vector3 target);
        void SetSpeed(float speed);
    }
}