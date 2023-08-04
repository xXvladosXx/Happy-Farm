using Codebase.Logic.Stats;
using Codebase.Logic.Storage;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Entity.ProductionEntities.Production
{
    public class Collectable : ICollectable, IComponent, IGameBehaviour
    {
        private readonly IItem _item;
        private readonly int _amount;
        private readonly IDestroyable _destroyable;
        private readonly IStorageUser _playerStorage;
        public bool WasCollected { get; private set; }
        public bool CanBeCollected { get; set; }

        public Collectable(IStorageUser playerStorage,
            IItem item, int amount, IDestroyable destroyable)
        {
            _playerStorage = playerStorage;
            _item = item;
            _amount = amount;
            _destroyable = destroyable;
        }

        public void Interact(Transform transform)
        {
            Collect(transform);
        }

        public bool GameUpdate()
        {
            if (WasCollected)
            {
                Recycle();
                return false;
            }
            
            return true;
        }

        public void Recycle() => _destroyable.Destroy();

        public void Collect(Transform transform)
        {
            if(!CanBeCollected)
                return;
                
            if(_playerStorage.Inventory.TryToAddToAnySlot(_item,_amount))
               WasCollected = true;
        }
    }
}