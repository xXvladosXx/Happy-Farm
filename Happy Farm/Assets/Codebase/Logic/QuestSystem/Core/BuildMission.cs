using System;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Unity.VisualScripting;

namespace Codebase.Logic.QuestSystem.Core
{
    public class BuildMission: Mission
    {
        private readonly BuildingMissionConfig _config;
        private readonly MissionRequires _missionRequires;

        private bool _isBuilt;
        
        public BuildMission(BuildingMissionConfig config,
            MissionRequires missionRequires) : base(config)
        {
            _config = config;
            _missionRequires = missionRequires;
        }

        protected override void OnStart() => 
            _missionRequires.BuildingRegistry.OnBuilt += OnBuildingBuilt;

        protected override void OnComplete() => 
            _missionRequires.BuildingRegistry.OnBuilt -= OnBuildingBuilt;

        protected override float GetProgress() =>
            Convert.ToSingle(_isBuilt);

        private void OnBuildingBuilt(BuildingTypeID buildingTypeID)
        {
            if(buildingTypeID == _config.BuildingTypeID)
                NotifyAboutStateChanged();
        }
    }
}