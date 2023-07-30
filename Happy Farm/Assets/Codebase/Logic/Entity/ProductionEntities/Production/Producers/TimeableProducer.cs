using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public abstract class TimeableProducer : IProducer
    {
        protected readonly IGameFactory GameFactory;
        private readonly float _productionTime;
        private float _currentTimeInProduction;
        public bool InProduction { get; private set; }
        public int Amount { get; private set; }
        public Transform Transform { get; }

        protected TimeableProducer(IGameFactory gameFactory,
            float productionTime,
            Transform transform,
            int amount)
        {
            Transform = transform;
            Amount = amount;
            GameFactory = gameFactory;
            _productionTime = productionTime;
        }
        
        private void StartProduction() =>
            InProduction = true;

        private void StopProduction() =>
            InProduction = false;
        
        public async UniTask Produce(int amount, Vector3 position)
        {
            Debug.Log($"Production started");
            StartProduction();

            while (_currentTimeInProduction < _productionTime)
            {
                _currentTimeInProduction += Time.deltaTime;
                await UniTask.Yield();
            }

            await Create(amount, position);
            StopProduction();
            _currentTimeInProduction = 0;
        }

        protected abstract UniTask Create(int amount, Vector3 position);
    }
}