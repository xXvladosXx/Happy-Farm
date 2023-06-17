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
        private readonly IContainer _playerStorage;

        public bool CanBeCollected { get; set; }

        public Collectable(IContainer playerStorage,
            IItem item)
        {
            _playerStorage = playerStorage;
            _item = item;
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
                
            if(_playerStorage.TryToAddToAnySlot(_item,1))
                Object.Destroy(transform.gameObject);
        }
    }
}