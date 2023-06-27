using System;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.Stats;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class AnimalEater : IEater
    {
        private readonly IProducer _producer;
        private readonly Transform _transform;
        public IEatable Eatable { get; set; }
        public Health Health { get; private set; }
        public float HungerThreshold { get; private set; }
        public float EatingRate { get; private set; }
        public float EatingAmount { get; private set; }
        public float HungerRate { get; private set; }
        public float HungerAmount { get; private set; }

        private float _timeSinceLastHunger;
        private float _timeSinceLastEating;

        public event Action OnHunger;
        public event Action OnAte;

        public AnimalEater(float hungerThreshold, float eatingRate,
            float eatingAmount, float hungerRate,
            Health health, IProducer producer,
            Transform transform, float hungerAmount)
        {
            _producer = producer;
            _transform = transform;
            HungerThreshold = hungerThreshold;
            Health = health;
            EatingRate = eatingRate;
            EatingAmount = eatingAmount;
            HungerRate = hungerRate;
            HungerAmount = hungerAmount;
        }

        public void Starve()
        {
            _timeSinceLastHunger += Time.deltaTime;
            if(_timeSinceLastHunger < HungerRate)
                return;
            
            Health.Decrease(HungerAmount);
            _timeSinceLastHunger = 0f;

            if (Health.CurrentHealth < HungerThreshold) 
                OnHunger?.Invoke();
        }

        public async void Eat()
        {
            _timeSinceLastEating += Time.deltaTime;
            if(_timeSinceLastEating < EatingRate)
                return;
            
            if (Eatable == null || Eatable.Equals(null))
                return;
            
            Health.Increase(EatingAmount);
            Eatable.Consume(EatingAmount);
            _timeSinceLastEating = 0f;

            if (Health.CurrentHealth >= Health.MaxHealth)
            {
                OnAte?.Invoke();
                await _producer.Produce(_producer.Amount, _transform.position);
            }
        }
    }
}