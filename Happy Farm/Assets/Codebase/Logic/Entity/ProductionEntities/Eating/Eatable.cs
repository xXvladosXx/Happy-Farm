using System;
using Codebase.Logic.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class Eatable : IEatable
    {
        public Transform Transform { get; private set; }
        
        private readonly Health _health;
        private readonly IDestroyable _destroyable;
        private readonly EatableHealthData _healthData;

        private float _currentTime;
        
        public Eatable(Health health,
            Transform transform,
            IDestroyable destroyable,
            EatableHealthData healthData)
        {
            _health = health;
            Transform = transform;
            _destroyable = destroyable;
            _healthData = healthData;
        }

        public void Consume(float amount)
        {
            _health.Decrease(amount);
        }

        public bool GameUpdate()
        {
            _currentTime += Time.deltaTime;
            
            if (_health.CurrentHealth > 0)
            {
                if (_currentTime > _healthData.HealthDecreaseInterval)
                {
                    _health.Decrease(_healthData.HealthDecrease);
                    _currentTime = 0;
                }
                
                return true;
            }
            
            Recycle();
            return false;
        }

        public void Recycle()
        {
            _destroyable.Destroy();   
        }
    }
}