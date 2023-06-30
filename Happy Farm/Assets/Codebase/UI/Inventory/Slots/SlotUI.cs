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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                print("Left click");
            }
            else
            {
                print("Right click");
            }
        }

        public void Refresh()
        {
            _itemUI.Refresh(_slot.Item, _slot.CurrentAmount);
        }

        public void Construct(ISlot slot)
        {
            _slot = slot;
        }
    }
}