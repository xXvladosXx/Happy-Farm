using System;
using Codebase.Logic.Stats;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public interface IEater
    {
        void Starve();
        void Eat();
        float HungerRate { get; }
        float EatingRate { get; }
        Health Health { get; }
        IEatable Eatable { get; set; }
        float EatingAmount { get; }
        float HungerAmount { get; }
        event Action OnHunger;
        event Action OnAte;
    }
}