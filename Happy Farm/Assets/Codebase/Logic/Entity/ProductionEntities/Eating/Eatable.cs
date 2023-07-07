using System;
using Codebase.Logic.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class Eatable : SerializedMonoBehaviour, IEatable
    {
        public Health Health { get; private set; }
        public Transform Transform => transform;

        public void Construct(Health health)
        {
            Health = health;
        }

        public void Consume(float amount)
        {
            Health.Decrease(amount);
        }
    }
}