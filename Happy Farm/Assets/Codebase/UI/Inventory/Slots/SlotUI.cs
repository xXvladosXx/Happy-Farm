using Codebase.Logic.ShopSystem;
using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Codebase.UI.Inventory.Slots
{
    public class SlotUI : SerializedMonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ItemUI _itemUI;
        
        private ISlot _slot;
        private IShop _shop;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                print("Left click");
            }
            else
            {
                print("Right click");
                if (_shop.WasSold(_slot.Item.Price * _slot.CurrentAmount))
                {
                    _slot.RemoveItem(_slot.Capacity);
                    Refresh();
                }
            }
        }

        public void Refresh()
        {
            _itemUI.Refresh(_slot.Item, _slot.CurrentAmount);
        }

        public void Construct(ISlot slot, IShop shop)
        {
            _slot = slot;
            _shop = shop;
        }
    }
}