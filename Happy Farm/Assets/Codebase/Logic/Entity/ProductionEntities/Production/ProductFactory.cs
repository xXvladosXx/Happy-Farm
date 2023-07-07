using Codebase.Logic.Entity.ProductionEntities.Production.Consumers;
using Codebase.Logic.Entity.ProductionEntities.Production.Producers;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class ProductFactory : IComponent
    {
        private readonly IProducer _producer;
        private readonly IConsumer<string> _consumer;

        public ProductFactory(IProducer producer,
            IConsumer<string> consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }

        public void Interact(Transform transform)
        {
            var consumed = _consumer.Consume();
            if(consumed > 0 && _producer.InProduction == false)
            {
                _consumer.Consume();
                _producer.Produce(consumed, Vector3.back);
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