using System;
using Codebase.Logic.Entity.Stats;
using Sirenix.OdinInspector;

namespace Codebase.Logic.Entity
{
    public class HealthDestroyable : SerializedMonoBehaviour, IDestroyable
    {
        public Health Health { get; private set; }
        public event Action OnDestroyed;

        public void Construct(Health health)
        {
            Health = health;
        }

        public void Initialize()
        {
            Health.OnDied += Destroy;
        }
        
        public void Destroy()
        {
            Health.OnDied -= Destroy;
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}