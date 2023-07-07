using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public class InstantItemProducer : IProducer
    {
        private readonly IGameFactory _gameFactory;
        private readonly string _productId;
        private readonly int _productionAmount;
        public int Amount { get; private set; }
        public bool InProduction { get; }

        public InstantItemProducer(IGameFactory gameFactory,
            string productId,
            int productionAmount)
        {
            _gameFactory = gameFactory;
            _productId = productId;
            Amount = productionAmount;
        }
        
        public async UniTask Produce(int amount, Vector3 position)
        {
             await _gameFactory.CreateProduct(_productId, position, amount);
        }
    }
}