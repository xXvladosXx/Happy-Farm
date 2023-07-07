using System;

namespace Codebase.Logic.Stats
{
    public interface IDestroyable
    {
        public event Action<IDestroyable> OnDestroyed;
        void Destroy();
    }
}