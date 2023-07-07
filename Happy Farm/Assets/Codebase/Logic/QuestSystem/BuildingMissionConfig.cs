using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.QuestSystem.Core;
using UnityEngine;

namespace Codebase.Logic.QuestSystem
{
    [CreateAssetMenu(fileName = "BuildingMissionConfig", menuName = "Mission/BuildingMissionConfig")]
    public class BuildingMissionConfig : MissionConfig
    {
        public BuildingTypeID BuildingTypeID;
        public override Mission CreateMission(MissionRequires missionRequires) => 
            new BuildMission(this, missionRequires);
    }
}