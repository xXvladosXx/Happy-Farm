﻿using System;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity;
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
            _missionRequires.GameBehaviour.OnGameBehaviourAdded += OnEntityAdded;

        protected override void OnComplete() => 
            _missionRequires.GameBehaviour.OnGameBehaviourAdded -= OnEntityAdded;

        protected override float GetProgress() =>
            Convert.ToSingle(_isBuilt);

        private void OnEntityAdded(IGameBehaviour gameBehaviour)
        {
            if(gameBehaviour is Construction construction)
                CheckBuilding(construction.BuildingTypeID);
        }

        private void CheckBuilding(BuildingTypeID constructionBuildingTypeID)
        {
            if (constructionBuildingTypeID == _config.BuildingTypeID)
            {
                _isBuilt = true;
                NotifyAboutStateChanged();
            }
        }
    }
}