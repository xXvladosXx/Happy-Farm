using System.Collections.Generic;
using System.Linq;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Stats;
using Zenject;

namespace Codebase.Logic.Entity.EnemyEntities
{
    public class EnemyAnimalRegistry : ITickable
    {
        private Dictionary<IDestroyable, EnemyAnimal> Animals { get; } = new();
        private List<EnemyAnimal> EnemyAnimals { get; set; } = new();

        public void Tick()
        {
            for (int i = 0; i < EnemyAnimals.Count; i++)
            {
                EnemyAnimals[i].Update();
            }
        }

        public void Register(IDestroyable destroyable,
            EnemyAnimal productionAnimal)
        {
            Animals.Add(destroyable, productionAnimal);
            EnemyAnimals = Animals.Values.ToList();
            destroyable.OnDestroyed += Unregister;
        }

        private void Unregister(IDestroyable obj)
        {
            obj.OnDestroyed -= Unregister;
            Animals.Remove(obj);
            EnemyAnimals = Animals.Values.ToList();
        }

        public void Clear()
        {
            Animals.Clear();
        }
    }
}