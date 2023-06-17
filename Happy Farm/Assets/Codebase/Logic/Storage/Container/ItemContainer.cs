using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    [Serializable]
    public class ItemContainer : IContainer
    {
        [field: SerializeField] public List<ISlot> Slots { get; private set; }
        [field: SerializeField] public int Capacity { get; private set; }
        public bool IsFull => Slots.All(slot => slot.IsFull);

        public ItemContainer(int capacity)
        {
            Slots = new List<ISlot>();
            Capacity = capacity;

            for (int i = 0; i < capacity; i++)
            {
                Slots.Add(new Slot());
            }
        }

        public ItemContainer(List<ISlot> slots, int capacity)
        {
            Slots = slots;
            Capacity = capacity;
        }

        public IItem[] GetAllItems()
        {
            return new IItem[10];
        }

        public bool TryToAddToAnySlot(IItem item, int number)
        {
            if (IsFull)
                return false;

            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }

            if (AddItemToSlot(i, item, number))
            {
                return true;
            }

            return false;
        }

        public void RemoveFromSlot(int slot, int number)
        {
            Slots[slot].RemoveItem(number);
        }

        public void RemoveItem(string itemId, int number)
        {
            var slot = Slots.FindIndex(s => s.Item != null && s.Item.ItemID == itemId);
            RemoveFromSlot(slot, number);
        }

        public void DropFromSlot(int index, int number)
        {
            RemoveFromSlot(index, number);
        }

        public bool AddItemToSlot(int slot, IItem item, int number)
        {
            if (Slots[slot].Item == null)
            {
                Slots[slot].SetItem(item, number);
            }
            else
            {
                var amount = number + Slots[slot].CurrentAmount;

                if (amount > Slots[slot].Item.MaxItemsInStack)
                {
                    var restAmount = amount - Slots[slot].Item.MaxItemsInStack;
                    var emptySlot = FindEmptySlot();
                    number -= restAmount;

                    AddItemToSlot(emptySlot, item, restAmount);
                }

                Slots[slot].UpdateAmount(number);
            }

            return true;
        }

        public bool HasItem(IItem item) =>
            Slots.Any(slot => ReferenceEquals(slot.Item, item));

        public IItem GetItemInSlot(int index) =>
            Slots[index].Item;

        public int GetNumberInSlot(int slot) =>
            Slots[slot].CurrentAmount;

        public Tuple<bool, int> HasSpaceFor(IItem item, int index)
        {
            if (Slots[index].Item == null)
            {
                return new Tuple<bool, int>(true, Slots[index].Capacity);
            }

            if (!Slots[index].Item.IsStackable)
            {
                return new Tuple<bool, int>(false, 1);
            }

            return new Tuple<bool, int>(Slots[index].Item == item && !Slots[index].IsFull,
                Slots[index].Item.MaxItemsInStack);
        }

        private int FindSlot(IItem item)
        {
            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }

            return i;
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].Item == null)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindStack(IItem item)
        {
            if (!item.IsStackable)
            {
                return -1;
            }

            for (int i = 0; i < Slots.Count; i++)
            {
                if (ReferenceEquals(Slots[i].Item, item))
                {
                    if (Slots[i].IsFull)
                    {
                        continue;
                    }

                    return i;
                }
            }

            return -1;
        }

        public bool HasItem(string itemId, int amount)
        {
            foreach (var slot in Slots)
            {
                if (slot.Item != null && slot.Item.ItemID == itemId && slot.CurrentAmount >= amount)
                    return true;
            }

            return false;
        }

        public void IncreaseCapacity(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                Slots.Add(new Slot());
            }

            Capacity = capacity;
        }

        [Button]
        public void Initialize()
        {
            Slots = new List<ISlot>();
            for (int i = 0; i < Capacity; i++)
            {
                Slots.Add(new Slot());
            }
        }

        [Button]
        public void Refresh()
        {
        }
    }
}