using Codebase.Logic.Storage;
using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Utils.Cheat
{
    public class Cheater : MonoBehaviour
    {
        private IStorageUser _storageUser;

        public void Construct(IStorageUser storageUser)
        {
            _storageUser = storageUser;
        }
        
        [Button]
        public void AddItem(IItem item)
        {
            _storageUser.Inventory.TryToAddToAnySlot(item, 1);
        }
    }
}