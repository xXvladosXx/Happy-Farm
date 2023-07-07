using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Logic.Stats
{
    public class HealthDestroyable : IDestroyable
    {
        public Health Health { get; set; }
        public GameObject GameObject { get; private set; }
        public event Action<IDestroyable> OnDestroyed;

        public HealthDestroyable(Health health,
            GameObject gameObject)
        {
            Health = health;
            GameObject = gameObject;
        }

        public void Initialize()
        {
            Health.OnDied += Destroy;
        }
        
        public void Destroy()
        {
            Health.OnDied -= Destroy;
            OnDestroyed?.Invoke(this);
            Object.Destroy(GameObject);
        }
    }
}