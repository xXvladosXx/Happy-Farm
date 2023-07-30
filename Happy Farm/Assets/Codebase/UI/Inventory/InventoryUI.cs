using System.Collections.Generic;
using Codebase.Logic.ShopSystem;
using Codebase.Logic.Storage.Container;
using Codebase.UI.Inventory.Slots;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.UI.Inventory
{
    public class InventoryUI : SerializedMonoBehaviour
    {
        [SerializeField] private SlotUI _slotPrefab;
        [SerializeField] private Transform _slotContainer;
        
        private readonly List<SlotUI> _slots = new List<SlotUI>();
        private IContainer _container;
        private IShop _shop;
        
        public void Construct(IContainer container, IShop shop)
        {
            if(container == null)
                return;

            _container = container;
            _shop = shop;
            ConstructUI();
        }

        public void Initialize()
        {
            _container.OnContainerUpdated += ConstructUI;
        }
        
        public void Dispose()
        {
            _container.OnContainerUpdated -= ConstructUI;
        }
        
        private void ConstructUI()
        {
            if (_slots.Count < _container.Slots.Count)
            {
                for (int i = 0; i < _container.Slots.Count; i++)
                {
                    if (i < _slots.Count)
                    {
                        _slots[i].Refresh();
                    }
                    else
                    {
                        var slot = Instantiate(_slotPrefab, _slotContainer);
                        slot.Construct(_container.Slots[i], _shop);
                        _slots.Add(slot);
                    }                
                }
            }

            foreach (var slot in _slots)
            {
                slot.Refresh();
            }
        }
    }
}