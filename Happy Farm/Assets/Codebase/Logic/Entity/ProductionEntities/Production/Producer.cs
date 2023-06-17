using Codebase.Gameplay;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class Producer : IProducer
    {
        private readonly GameFactory _gameFactory;
        private readonly string _productId;
        public bool InProduction { get; private set; }

        public Producer(GameFactory gameFactory,
            string productId)
        {
            _gameFactory = gameFactory;
            _productId = productId;
        }
        
        public void StartProduction() => 
            InProduction = true;

        public void StopProduction() => 
            InProduction = false;
        
        public async UniTask Produce(Vector3 position)
        {
            var product = await _gameFactory.CreateProduct(_productId, position);
        }
    }
}
