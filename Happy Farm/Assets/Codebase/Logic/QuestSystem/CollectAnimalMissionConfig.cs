using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Codebase.Logic.QuestSystem.Core;
using UnityEngine;

namespace Codebase.Logic.QuestSystem
{
    [CreateAssetMenu(fileName = "CollectAnimalMissionConfig", menuName = "Mission/CollectAnimalMissionConfig")]
    public class CollectAnimalMissionConfig : MissionConfig
    {
        public ProductionAnimalTypeID AnimalTypeID;
        public int Amount;

        public override Core.Mission CreateMission(MissionRequires missionRequires)
            => new CollectResourcesMission(null, missionRequires);
    }
}