using Codebase.Gameplay;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class Factory : IComponent
    {
        private readonly IProducer _producer;
        private readonly IConsumer _consumer;
        private readonly IStorageUser _storageUser;

        public Factory(IProducer producer,
            IConsumer consumer,
            IStorageUser storageUser)
        {
            _producer = producer;
            _consumer = consumer;
            _storageUser = storageUser;
        }

        public void Interact(Transform transform)
        {
            var itemAmount = _storageUser.Inventory.FindItemAmount(_consumer.ItemId);
            var maxAmount = _consumer.Amount;
            var clamp = Mathf.Clamp(itemAmount, 0, maxAmount);

            if(clamp > 0 && _producer.InProduction == false)
            {
                _storageUser.Inventory.RemoveItem(_consumer.ItemId, clamp);
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