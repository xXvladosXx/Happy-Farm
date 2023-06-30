using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.QuestSystem
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "Mission/MissionConfig")]
    public abstract class MissionConfig : SerializedScriptableObject
    {
        public string Id;
        public string Title;
        public Sprite Icon;
        public abstract Core.Mission CreateMission(MissionRequires missionRequires);
    }
}