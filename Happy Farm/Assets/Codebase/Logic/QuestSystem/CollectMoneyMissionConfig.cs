using UnityEngine;

namespace Codebase.Logic.QuestSystem
{
    [CreateAssetMenu(fileName = "CollectMoneyMissionConfig", menuName = "Mission/CollectMoneyMissionConfig")]
    public class CollectMoneyMissionConfig : MissionConfig
    {
        public int Amount;
        public override Core.Mission CreateMission(MissionRequires missionRequires)
        {
            return null;
        }
    }
}