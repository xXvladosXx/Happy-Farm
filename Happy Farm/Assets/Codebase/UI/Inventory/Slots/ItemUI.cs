using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Inventory.Slots
{
    public class ItemUI : SerializedMonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amount;
        
        public void Refresh(IItem item, int amount)
        {
            if(item == null)
            {
                _icon.sprite = null;
                _amount.text = string.Empty;
                return;
            }
            
            _icon.sprite = item.Icon;
            _amount.text = amount.ToString();
        }
    }
}