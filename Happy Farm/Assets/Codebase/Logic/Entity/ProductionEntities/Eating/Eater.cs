﻿using System;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.Production.Producers;
using Codebase.Logic.Stats;
using Codebase.Logic.TimeManagement;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class Eater : IEater, IPauseHandler
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
        private bool _isPaused;

        public event Action OnHunger;
        public event Action OnAte;

        public Eater(float hungerThreshold, float eatingRate,
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
            if(_isPaused)
                return;
            
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
            if(_isPaused)
                return;
            
            _timeSinceLastEating += Time.deltaTime;
            if(_timeSinceLastEating < EatingRate)
                return;
            
            if (Eatable == null || Eatable.Equals(null) || Eatable.Transform == null)
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

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
        }
    }
}