using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public interface IEatable
    {
        Transform Transform { get; }
        void Consume(float amount);
    }
}