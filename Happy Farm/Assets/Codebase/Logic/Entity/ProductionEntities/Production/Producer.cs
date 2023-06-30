using Codebase.Gameplay;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class TimeProducer : IProducer
    {
        public float ProductionTime { get; private set; }
        public Transform Transform { get; }
        public bool InProduction { get; private set; }
        public int Amount { get; }

        private readonly IGameFactory _gameFactory;
        private readonly string _productId;

        private float _currentTimeInProduction;

        public TimeProducer(IGameFactory gameFactory,
            string productId,
            float productionTime)
        {
            _gameFactory = gameFactory;
            _productId = productId;
            ProductionTime = productionTime;
        }

        public void StartProduction() =>
            InProduction = true;

        public void StopProduction() =>
            InProduction = false;

        public async UniTask Produce(int amount, Vector3 position)
        {
            Debug.Log($"Production started {_productId}");
            StartProduction();
            
            while (_currentTimeInProduction < ProductionTime)
            {
                _currentTimeInProduction += Time.deltaTime;
                await UniTask.Yield();
            }

            await _gameFactory.CreateProduct(_productId, position, amount);
            StopProduction();
            _currentTimeInProduction = 0;
        }
    }
}