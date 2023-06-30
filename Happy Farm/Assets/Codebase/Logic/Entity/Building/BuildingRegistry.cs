using System.Collections.Generic;
using Codebase.Infrastructure.StaticData;

namespace Codebase.Logic.Entity.Building
{
    public class BuildingRegistry
    {
        public List<BuildingTypeID> Buildings { get; } = new List<BuildingTypeID>();
        
        public void Register(BuildingTypeID buildingTypeID)
        {
            Buildings.Add(buildingTypeID);
        }
        
        public void Unregister(BuildingTypeID buildingTypeID)
        {
            Buildings.Remove(buildingTypeID);
        }
        
        public void Clear()
        {
            Buildings.Clear();
        }
    }
}