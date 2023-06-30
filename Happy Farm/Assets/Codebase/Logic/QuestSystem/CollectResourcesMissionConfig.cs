using Codebase.Logic.QuestSystem.Core;
using Codebase.Logic.Storage.Container;
using UnityEngine;

namespace Codebase.Logic.QuestSystem
{
    [CreateAssetMenu(fileName = "CollectResourcesMissionConfig", menuName = "Mission/CollectResourcesMissionConfig")]
    public class CollectResourcesMissionConfig : MissionConfig
    {
        public IItem Item;
        public int Amount;
        
        public override Core.Mission CreateMission(MissionRequires missionRequires)
            => new CollectResourcesMission(this, missionRequires);
    }
}