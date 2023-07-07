using System.Collections.Generic;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Stats;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities
{
    public class AnimalRegistry : ITickable
    {
        public Dictionary<IDestroyable, ProductionAnimal> Animals { get; } = new();

        public void Tick()
        {
            foreach (var animal in Animals.Values) 
                animal.Update();
        }

        public void Register(IDestroyable destroyable,
            ProductionAnimal productionAnimal)
        {
            Animals.Add(destroyable, productionAnimal);
            destroyable.OnDestroyed += Unregister;
        }

        private void Unregister(IDestroyable obj)
        {
            obj.OnDestroyed -= Unregister;
            Animals.Remove(obj);
        }

        public void Clear()
        {
            Animals.Clear();
        }
    }
}