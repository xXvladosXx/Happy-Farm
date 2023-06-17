using System.Collections.Generic;

namespace Codebase.Logic.Storage.Container
{
    public interface IContainer
    {
        List<ISlot> Slots { get; }

        int Capacity { get; }
        bool IsFull { get; }
        
        bool TryToAddToAnySlot(IItem item, int number);
        bool AddItemToSlot(int slot, IItem item, int number);
        void RemoveFromSlot(int slot, int number);
        void RemoveItem(string itemId, int number);
        IItem GetItemInSlot(int index);
        int GetNumberInSlot(int index);
        IItem[] GetAllItems();
        void DropFromSlot(int index, int number);
        void IncreaseCapacity(int capacity);
        bool HasItem(string itemId, int amount);
    }
}