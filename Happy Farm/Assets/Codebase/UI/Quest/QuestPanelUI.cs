using System;
using System.Collections.Generic;
using Codebase.Logic.QuestSystem.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.UI.Quest
{
    public class QuestPanelUI : SerializedMonoBehaviour
    {
        [SerializeField] private QuestUI _questPrefab;
        
        private List<QuestUI> _quests = new List<QuestUI>();

        public void Initialize(List<Mission> missions)
        {
            foreach (var mission in missions)
            {
                var quest = Instantiate(_questPrefab, transform);
                quest.Refresh(mission);
                _quests.Add(quest);
                
                mission.OnStateChanged += quest.Refresh;
                mission.OnCompleted += quest.End;
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var quest in _quests)
            {
                Destroy(quest.gameObject);
            }
            
            _quests.Clear();
        }
    }
}