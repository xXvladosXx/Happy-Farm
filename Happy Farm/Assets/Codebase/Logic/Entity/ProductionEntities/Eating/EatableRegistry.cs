using System.Collections.Generic;
using Codebase.Logic.Stats;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class EatableRegistry : ITickable
    {
        public Dictionary<IDestroyable, IEatable> Eatables { get; } = new();
        
        public void Register(IDestroyable destroyable,
            IEatable eatable)
        {
            Eatables.Add(destroyable, eatable);
            destroyable.OnDestroyed += Unregister;
        }

        private void Unregister(IDestroyable destroyable)
        {
            destroyable.OnDestroyed -= Unregister;
            Eatables.Remove(destroyable);
        }

        public void Clear()
        {
            Eatables.Clear();
        }

        public void Tick()
        {
            
        }
    }
}