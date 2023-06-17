using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Storage
{
    [CreateAssetMenu(menuName = "InventorySystem/StartStorage")]
    public class PlayerStorage : SerializedScriptableObject
    {
        [field: SerializeField] public IContainer Container { get; private set; }

        [Button]
        public void UpgradeStorage()
        {
            Container.IncreaseCapacity(10);
        }
    }
}