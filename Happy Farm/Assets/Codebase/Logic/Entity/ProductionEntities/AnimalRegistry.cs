using System.Collections.Generic;
using System.Linq;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Stats;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities
{
    public class AnimalRegistry : ITickable
    {
        private Dictionary<IDestroyable, ProductionAnimal> Animals { get; } = new();
        private List<ProductionAnimal> ProductionAnimals { get; set; } = new();

        public void Tick()
        {
            for (int i = 0; i < ProductionAnimals.Count; i++)
            {
                ProductionAnimals[i].Update();
            }
        }

        public void Register(IDestroyable destroyable,
            ProductionAnimal productionAnimal)
        {
            Animals.Add(destroyable, productionAnimal);
            ProductionAnimals = Animals.Values.ToList();
            destroyable.OnDestroyed += Unregister;
        }

        private void Unregister(IDestroyable obj)
        {
            obj.OnDestroyed -= Unregister;
            Animals.Remove(obj);
            ProductionAnimals = Animals.Values.ToList();
        }

        public void Clear()
        {
            Animals.Clear();
        }
    }
}