using Codebase.Logic.Entity.ProductionEntities.Production.Consumers;
using Codebase.Logic.Entity.ProductionEntities.Production.Producers;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class ProductFactory<T> : IComponent
    {
        private readonly TimeableProducer _producer;
        private readonly IConsumer<T, int> _consumer;

        public ProductFactory(TimeableProducer producer,
            IConsumer<T, int> consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }

        public async void Interact(Transform transform)
        {
            if (!_consumer.CanConsume(_consumer.MaxAmount))
                return;
            
            if(_producer.InProduction == false)
            {
                var consumed = _consumer.Consume(_consumer.MaxAmount);
                await _producer.Produce(consumed, _producer.Transform.position);
            }
            else
            {
                Debug.Log($"Can not start production from {_consumer.GetProduct()}");
            }
        }

        public void Update()
        {
        }
    }
}