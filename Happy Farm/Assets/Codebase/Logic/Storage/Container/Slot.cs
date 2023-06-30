using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Logic.Storage.Container
{
    [Serializable]
    public class Slot : ISlot
    {
        [field: SerializeField] public int Capacity { get; private set; }
        [field: SerializeField] public IItem Item { get; private set; }

        public bool IsFull
        {
            get
            {
                if (Item != null)
                {
                    return CurrentAmount >= Item.MaxItemsInStack;
                }

                return false;
            }
        }

        public bool IsEmpty => 
            Item == null || CurrentAmount == 0;

        [field: SerializeField] public int CurrentAmount { get; private set; }

        public Slot()
        {
            Item = null;
            Capacity = 64;
            CurrentAmount = 0;
        }

        public Slot(IItem item, int amount)
        {
            Item = item;
            CurrentAmount = amount;
            Capacity = item.MaxItemsInStack;
        }

        public void SetItem(IItem item, int amount)
        {
            Item = item;
            CurrentAmount = amount;
        }

        public void UpdateAmount(int amount)
        {
            CurrentAmount += amount;
        }
        
        public void RemoveItem(int number)
        {
            CurrentAmount -= number;
            if (CurrentAmount <= 0)
            {
                CurrentAmount = 0;
                Item = null;
            }
        }

        public void Clear()
        {
            if (IsEmpty)
                return;

            Item = null;
            CurrentAmount = 0;
        }
    }
}