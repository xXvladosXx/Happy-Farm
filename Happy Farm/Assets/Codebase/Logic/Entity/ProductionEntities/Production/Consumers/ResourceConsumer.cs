using Codebase.Logic.Entity.ProductionEntities.Production.Resource;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Consumers
{
    public class ResourceConsumer : IConsumer<ResourceType, int>
    {
        public int MaxAmount { get; }
        
        private readonly ResourceType _resourceType;
        private readonly IResourcesStorage _resourcesStorage;

        public ResourceConsumer(ResourceType resourceType,
            IResourcesStorage resourcesStorage,
            int maxAmount)
        {
            MaxAmount = maxAmount;
            _resourceType = resourceType;
            _resourcesStorage = resourcesStorage;
        }
        
        public ResourceType GetProduct() => _resourceType;

        public bool CanConsume(int amount) => 
            _resourcesStorage.HasResource(_resourceType, amount);

        public int Consume(int amount)
        {
            _resourcesStorage.Remove(_resourceType, amount);
            return amount;
        }
    }
}