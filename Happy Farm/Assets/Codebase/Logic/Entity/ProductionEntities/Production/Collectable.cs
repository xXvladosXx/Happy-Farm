using Codebase.Logic.Storage;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class Collectable : ICollectable, IComponent
    {
        private readonly IItem _item;
        private readonly int _amount;
        private readonly IStorageUser _playerStorage;

        public bool CanBeCollected { get; set; }

        public Collectable(IStorageUser playerStorage,
            IItem item, int amount)
        {
            _playerStorage = playerStorage;
            _item = item;
            _amount = amount;
        }

        public void Interact(Transform transform)
        {
            Collect(transform);
        }

        public void Update() { }

        public void Collect(Transform transform)
        {
            if(!CanBeCollected)
                return;
                
            if(_playerStorage.Inventory.TryToAddToAnySlot(_item,_amount))
                Object.Destroy(transform.gameObject);
        }
    }
}