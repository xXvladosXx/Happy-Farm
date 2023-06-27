using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public interface ICollectable
    {
        bool CanBeCollected { get; set; }
        void Collect(Transform transform);
    }
}