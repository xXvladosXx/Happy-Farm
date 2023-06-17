using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    public interface IItem
    {
        string Description { get; }
        Sprite Icon { get; }
        string Name { get; }
        GameObject WorldPrefab { get; }
        bool IsStackable { get; }
        int MaxItemsInStack { get; }
        string ItemID { get; }
    }
}