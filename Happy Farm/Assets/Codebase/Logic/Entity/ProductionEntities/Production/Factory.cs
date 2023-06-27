using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class Factory : IComponent
    {
        private readonly IProducer _producer;
        private readonly IConsumer _consumer;
        private readonly IContainer _container;

        public Factory(IProducer producer,
            IConsumer consumer,
            IContainer container)
        {
            _producer = producer;
            _consumer = consumer;
            _container = container;
        }

        public void Interact(Transform transform)
        {
            var itemAmount = _container.FindItemAmount(_consumer.ItemId);
            var maxAmount = _consumer.Amount;
            var clamp = Mathf.Clamp(itemAmount, 0, maxAmount);

            if(clamp > 0 && _producer.InProduction == false)
            {
                _container.RemoveItem(_consumer.ItemId, clamp);
                _producer.Produce(clamp, Vector3.back);
            }
            else
            {
                Debug.Log($"Can not start production from {_consumer.ItemId}");
            }
        }

        public void Update()
        {
        }
    }
}