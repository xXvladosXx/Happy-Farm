using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Logic.QuestSystem.Core
{
    public class MissionsCollector : IInitializable
    {
        private readonly MissionCatalog _missionCatalog;
        private readonly MissionRequires _missionRequires;
        public readonly List<Mission> Missions = new();

        public event Action<Mission> OnRewardReceived; 
        public event Action<Mission> OnMissionChanged; 
        public event Action OnCompleted;

        public MissionsCollector(MissionCatalog missionCatalog,
            MissionRequires missionRequires)
        {
            _missionCatalog = missionCatalog;
            _missionRequires = missionRequires;
            Start();
        }
        
        private void Start()
        {
            foreach (var missionConfig in _missionCatalog.Missions)
            {
                var config = missionConfig.CreateMission(_missionRequires);
                config.Start();
                Missions.Add(config);
            }
        }

        public void Initialize()
        {
            foreach (var mission in Missions)
            {
                mission.OnCompleted += OnMissionCompleted;
            }
        }

        private void OnMissionCompleted(Mission mission)
        {
            mission.OnCompleted -= OnMissionCompleted;
            if (Missions.Any(mis => !mis.IsCompleted))
                return;

            OnCompleted?.Invoke();
        }

        public void ReceiveReward()
        {
            this.ReceiveReward(null);
        }

        public void ReceiveReward(Mission mission)
        {
            if (!mission.IsCompleted)
            {
                throw new Exception($"Can not receive reward for not completed mission: {mission.Id}!");
            }
        }
    }
}