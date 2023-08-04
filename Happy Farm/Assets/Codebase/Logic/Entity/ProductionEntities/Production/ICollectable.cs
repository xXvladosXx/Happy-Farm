using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public interface ICollectable
    {
        bool CanBeCollected { get; set; }
        bool WasCollected { get; }
        void Collect(Transform transform);
    }
}