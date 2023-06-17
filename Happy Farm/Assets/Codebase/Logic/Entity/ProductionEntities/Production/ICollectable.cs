using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    public interface ICollectable
    {
        bool CanBeCollected { get; set; }
        void Collect(Transform transform);
    }
}