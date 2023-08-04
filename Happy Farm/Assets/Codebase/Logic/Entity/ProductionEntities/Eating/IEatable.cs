using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public interface IEatable : IGameBehaviour
    {
        Transform Transform { get; }
        void Consume(float amount);
    }
}