using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    public interface IItem
    {
        Sprite Icon { get; }
        string Name { get; }
        bool IsStackable { get; }
        int MaxItemsInStack { get; }
        string ItemID { get; }
    }
}