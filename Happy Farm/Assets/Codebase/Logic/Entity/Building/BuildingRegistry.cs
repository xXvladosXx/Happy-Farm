using System;
using System.Collections.Generic;
using Codebase.Infrastructure.StaticData;
using Zenject;

namespace Codebase.Logic.Entity.Building
{
    public class BuildingRegistry : ITickable
    {
        public List<BuildingTypeID> Buildings { get; } = new List<BuildingTypeID>();
        public List<ProductionConstruction> ProductionConstructions { get; } = new();

        public event Action<BuildingTypeID> OnBuilt;
        
        public void Register(BuildingTypeID buildingTypeID,
            ProductionConstruction productionConstruction)
        {
            Register(buildingTypeID);
            ProductionConstructions.Add(productionConstruction);
        }

        public void Register(BuildingTypeID buildingTypeID)
        {
            Buildings.Add(buildingTypeID);
            OnBuilt?.Invoke(buildingTypeID);
        }

        public void Unregister(BuildingTypeID buildingTypeID, 
            ProductionConstruction productionConstruction)
        {
            ProductionConstructions.Remove(productionConstruction);
            Unregister(buildingTypeID);
        }
        
        public void Unregister(BuildingTypeID buildingTypeID)
        {
            Buildings.Remove(buildingTypeID);
        }
        
        public void Clear()
        {
            Buildings.Clear();
        }

        public void Tick()
        {
            foreach (var productionConstruction in ProductionConstructions)
                productionConstruction.Update();
        }
    }
}