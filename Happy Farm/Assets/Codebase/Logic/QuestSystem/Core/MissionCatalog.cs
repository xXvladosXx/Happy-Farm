using UnityEngine;

namespace Codebase.Logic.QuestSystem.Core
{
    [CreateAssetMenu(fileName = "MissionCatalog", menuName = "Mission/New MissionCatalog")]
    public class MissionCatalog : ScriptableObject
    {
        public MissionConfig[] Missions;
    }
}