using System;
using UnityEngine;

namespace Codebase.Logic.Stats
{
    public class Health
    {
        public readonly float MaxHealth;
        public float CurrentHealth { get; private set; }

        public event Action OnDied;

        public Health(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void Decrease(float amount)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            if (CurrentHealth <= 0)
                OnDied?.Invoke();
        }
        
        public void Increase(float amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        }
    }
}