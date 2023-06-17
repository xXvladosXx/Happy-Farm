using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    [CreateAssetMenu(menuName = "InventorySystem/Item")]
    public class Item : SerializedScriptableObject, IItem
    {
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public GameObject PickupPrefab { get; private set; }
        [field: SerializeField] public GameObject WorldPrefab { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }
        [field: SerializeField] public int MaxItemsInStack { get; private set; }
        [field: SerializeField] public string ItemID { get; private set; }
    }
}