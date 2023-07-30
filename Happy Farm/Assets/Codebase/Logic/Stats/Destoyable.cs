using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Logic.Stats
{
    public class Destoyable : IDestroyable
    {
        public GameObject GameObject { get; private set; }
        public event Action<IDestroyable> OnDestroyed;

        public Destoyable(GameObject gameObject)
        {
            GameObject = gameObject;
        }
        
        public void Destroy()
        {
            OnDestroyed?.Invoke(this);
            Object.Destroy(GameObject);
        }
    }
}