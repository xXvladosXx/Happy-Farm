using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public class TimeableItemProducer : TimeableProducer
    {
        private readonly string _productId;

        private float _currentTimeInProduction;

        public TimeableItemProducer(IGameFactory gameFactory,
            string productId,
            float productionTime,
            Transform transform,
            int amount) : base(gameFactory, productionTime, transform, amount)
        {
            _productId = productId;
        }

        protected override async UniTask Create(int amount, Vector3 position)
        {
            await GameFactory.CreateProduct(_productId, position, amount);
        }
    }
}