using System;
using System.Collections.Generic;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Stats;
using Zenject;

namespace Codebase.Logic.Entity.Building
{
    public class BuildingRegistry : ITickable
    {
        public Dictionary<IDestroyable, Construction> Constructions { get; } = new();
    
        public event Action<BuildingTypeID> OnBuilt;
        
        public void Register(BuildingTypeID buildingTypeID,
            Construction construction,
            IDestroyable destroyable)
        {
            Constructions.Add(destroyable, construction);
            destroyable.OnDestroyed += Unregister;
            OnBuilt?.Invoke(buildingTypeID);
        }

        public void Unregister(IDestroyable destroyable)
        {
            destroyable.OnDestroyed -= Unregister;
            Constructions.Remove(destroyable);
        }
        
        public void Clear()
        {
            Constructions.Clear();
        }

        public void Tick()
        {
            foreach (var productionConstruction in Constructions.Values)
                productionConstruction.Update();
        }
    }
}