using System;
using UnityEngine;

namespace Codebase.Logic.Entity
{
    public interface IDestroyable
    {
        public event Action OnDestroyed;
        void Destroy();
    }
}