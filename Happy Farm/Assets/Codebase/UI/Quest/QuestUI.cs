using Codebase.Logic.QuestSystem.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Quest
{
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private Image _completed;
        [SerializeField] private Image _questIcon;
        [SerializeField] private TextMeshProUGUI _questName;
        [SerializeField] private TextMeshProUGUI _questDescription;
        
        public void Refresh(Mission mission)
        {
            _questIcon.sprite = mission.Icon;
            _questName.text = mission.Title;
            _questDescription.text = mission.ToString();
            _completed.gameObject.SetActive(false);
        }

        public void End(Mission mission)
        {
            _questDescription.text = mission.ToString();
            _questDescription.fontStyle = FontStyles.Strikethrough;
            _questName.fontStyle = FontStyles.Strikethrough;
            
            _completed.gameObject.SetActive(true);
        }
    }
}