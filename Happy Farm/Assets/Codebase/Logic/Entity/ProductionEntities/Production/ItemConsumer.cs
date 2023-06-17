using System;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class ItemConsumer : IConsumer, IComponent
    {
        private readonly IContainer _container;
        private readonly string _itemId;
        private readonly int _amount;

        public event Action OnConsumed;
        public ItemConsumer(IContainer container,
            string itemId,
            int amount)
        {
            _container = container;
            _itemId = itemId;
            _amount = amount;
        }

        public void Interact(Transform transform)
        {
            TryToConsume();
        }

        public void Update()
        {
        }

        public bool TryToConsume()
        {
            if(_container.HasItem(_itemId, _amount))
            {
                _container.RemoveItem(_itemId, _amount);
                OnConsumed?.Invoke();
                return true;
            }

            return false;
        }
    }
}