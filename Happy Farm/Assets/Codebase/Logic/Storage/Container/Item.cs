using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    [CreateAssetMenu(menuName = "InventorySystem/Item")]
    public class Item : SerializedScriptableObject, IItem
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }
        [field: SerializeField] public int MaxItemsInStack { get; private set; }
        [field: SerializeField] public string ItemID { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}