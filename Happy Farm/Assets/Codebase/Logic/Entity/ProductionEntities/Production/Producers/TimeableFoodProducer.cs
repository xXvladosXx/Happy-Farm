using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public class TimeableFoodProducer : TimeableProducer
    {
        private readonly FoodRegistry _foodRegistry;

        public TimeableFoodProducer(IGameFactory gameFactory,
            int productionAmount,
            float productionTime,
            FoodRegistry foodRegistry) : base(gameFactory, productionTime)
        {
            _foodRegistry = foodRegistry;
            Amount = productionAmount;
        }

        protected override UniTask Create(int amount, Vector3 position)
        {
            _foodRegistry.Add(Amount);
            return UniTask.CompletedTask;
        }
    }
}