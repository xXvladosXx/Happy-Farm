using Codebase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production.Producers
{
    public abstract class TimeableProducer : IProducer
    {
        private readonly IGameFactory _gameFactory;
        private readonly float _productionTime;
        private float _currentTimeInProduction;
        public bool InProduction { get; private set; }
        public int Amount { get; set; }

        protected float ProductionTime { get; set; }

        public TimeableProducer(IGameFactory gameFactory,
            float productionTime)
        {
            _gameFactory = gameFactory;
            _productionTime = productionTime;
        }
        
        private void StartProduction() =>
            InProduction = true;

        private void StopProduction() =>
            InProduction = false;
        
        public virtual async UniTask Produce(int amount, Vector3 position)
        {
            Debug.Log($"Production started");
            StartProduction();

            while (_currentTimeInProduction < ProductionTime)
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