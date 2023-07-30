using Codebase.UI.AnimalSpawner;
using Codebase.UI.Inventory;
using Codebase.UI.Quest;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.UI
{
    public class GameplayUI : SerializedMonoBehaviour
    {
        [field: SerializeField] public SpawnerUI SpawnerUI { get; private set; }
        [field: SerializeField] public InventoryUI InventoryUI { get; private set; }
        [field: SerializeField] public QuestPanelUI QuestPanelUI { get; private set; }
    }
}