using System;
using Codebase.Logic.Entity.Stats;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public interface IEater
    {
        void Starve();
        void Eat(IEatable eatable);
        float HungerRate { get; }
        float EatingRate { get; }
        Health Health { get; }
        event Action OnHunger;
        event Action OnAte;
    }
}