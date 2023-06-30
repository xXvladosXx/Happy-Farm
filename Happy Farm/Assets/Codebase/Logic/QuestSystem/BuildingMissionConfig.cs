using Codebase.Infrastructure.StaticData;
using UnityEngine;

namespace Codebase.Logic.QuestSystem
{
    [CreateAssetMenu(fileName = "BuildingMissionConfig", menuName = "Mission/BuildingMissionConfig")]
    public class BuildingMissionConfig : MissionConfig
    {
        public BuildingTypeID BuildingTypeID;
        public override Core.Mission CreateMission(MissionRequires missionRequires)
        {
            return null;
        }
    }
}