using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Logic.QuestSystem.Core
{
    public class MissionsManager : IInitializable
    {
        private readonly MissionCatalog _missionCatalog;
        private readonly MissionRequires _missionRequires;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly List<Mission> _missions = new();

        public event Action<Mission> OnRewardReceived; 
        public event Action<Mission> OnMissionChanged; 
        public event Action OnCompleted;

        public MissionsManager(MissionCatalog missionCatalog,
            MissionRequires missionRequires,
            IGameStateMachine gameStateMachine)
        {
            _missionCatalog = missionCatalog;
            _missionRequires = missionRequires;
            _gameStateMachine = gameStateMachine;
            Start();
        }
        
        private void Start()
        {
            foreach (var missionConfig in _missionCatalog.Missions)
            {
                var config = missionConfig.CreateMission(_missionRequires);
                config.Start();
                _missions.Add(config);
            }
        }

        public void Initialize()
        {
            foreach (var mission in _missions)
            {
                mission.OnCompleted += OnMissionCompleted;
            }
        }

        private void OnMissionCompleted(Mission mission)
        {
            mission.OnCompleted -= OnMissionCompleted;
            if (_missions.Any(mis => !mis.IsCompleted))
                return;

            OnCompleted?.Invoke();
            _gameStateMachine.Enter<GameOverState>();
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

        private void GenerateMissions()
        {
            
        }
    }
}