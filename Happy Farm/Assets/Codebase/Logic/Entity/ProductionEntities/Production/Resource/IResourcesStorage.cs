using System;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Resource
{
    public interface IResourcesStorage
    {
        event Action<ResourceType, int, int> OnResourceChanged;
        bool HasResource(ResourceType type, int amount);
        void Add(ResourceType type, int amount);
        void Remove(ResourceType type, int amount);
        void IncreaseMaxAmount(ResourceType type, int amount);
    }
}