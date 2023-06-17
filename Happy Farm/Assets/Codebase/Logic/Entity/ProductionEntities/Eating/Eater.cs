using System;
using Codebase.Logic.Entity.Stats;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class Eater : IEater
    {
        public Health Health { get; private set; }
        public float HungerThreshold { get; private set; }
        public float EatingRate { get; private set; }
        public float HungerRate { get; private set; }
        
        private float _timeSinceLastHunger;
        private float _timeSinceLastEating;

        public event Action OnHunger;
        public event Action OnAte;

        public Eater(float hungerThreshold,
            float eatingRate,
            float hungerRate, 
            Health health)
        {
            HungerThreshold = hungerThreshold;
            Health = health;
            EatingRate = eatingRate;
            HungerRate = hungerRate;
        }

        public void Starve()
        {
            _timeSinceLastHunger += Time.deltaTime;
            if(_timeSinceLastHunger < HungerRate)
                return;
            
            Health.Decrease(HungerRate*8);
            _timeSinceLastHunger = 0f;

            if (Health.CurrentHealth < HungerThreshold) 
                OnHunger?.Invoke();
        }

        public void Eat(IEatable eatable)
        {
            _timeSinceLastEating += Time.deltaTime;
            if(_timeSinceLastEating < EatingRate)
                return;
            
            Health.Increase(EatingRate*20);
            eatable.Consume(EatingRate);
            _timeSinceLastEating = 0f;
            
            if(Health.CurrentHealth >= Health.MaxHealth)
                OnAte?.Invoke();
        }
    }
}