using System.Collections.Generic;
using Codebase.Logic.Stats;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    public class EatableRegistry : ITickable
    {
        public Dictionary<IDestroyable, Eatable> Eatables { get; } = new();
        
        public void Register(IDestroyable destroyable,
            Eatable eatable)
        {
            Eatables.Add(destroyable, eatable);
            destroyable.OnDestroyed += Unregister;
        }

        private void Unregister(IDestroyable obj)
        {
            obj.OnDestroyed -= Unregister;
            Eatables.Remove(obj);
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