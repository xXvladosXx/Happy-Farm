using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public class TimeableItemProducer : TimeableProducer
    {
        private readonly IGameFactory _gameFactory;
        private readonly string _productId;

        private float _currentTimeInProduction;

        public TimeableItemProducer(IGameFactory gameFactory,
            string productId,
            float productionTime) : base(gameFactory, productionTime)
        {
            _productId = productId;
        }

        protected override async UniTask Create(int amount, Vector3 position)
        {
            await _gameFactory.CreateProduct(_productId, position, Amount);
        }
    }
}