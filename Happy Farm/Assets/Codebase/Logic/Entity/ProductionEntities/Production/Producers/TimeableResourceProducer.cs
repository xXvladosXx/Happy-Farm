using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Utils.Raycast;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public class TimeableResourceProducer : TimeableProducer, IComponent 
    {
        private readonly ResourceType _resourceType;
        private readonly IResourcesStorage _resourcesStorage;

        public TimeableResourceProducer(IGameFactory gameFactory,
            int productionAmount,
            float productionTime,
            Transform transform,
            ResourceType resourceType,
            IResourcesStorage resourcesStorage) : base(gameFactory, productionTime, transform, productionAmount)
        {
            _resourceType = resourceType;
            _resourcesStorage = resourcesStorage;
        }

        protected override UniTask Create(int amount, Vector3 position)
        {
            _resourcesStorage.Add(_resourceType, amount);
            return UniTask.CompletedTask;
        }

        public async void Interact(Transform transform)
        {
            await Produce(Amount, Vector3.zero);
        }

        public void Update()
        {
        }
    }
}