using System;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StateMachine.States.Core;
using Codebase.Logic.QuestSystem.Core;
using Codebase.UI;
using Zenject;

namespace Codebase.Controllers
{
    public class QuestController : IInitializable, IDisposable
    {
        private readonly GameplayUI _gameplayUI;
        private readonly MissionsCollector _missionsCollector;
        private readonly IGameStateMachine _gameStateMachine;

        public QuestController(GameplayUI gameplayUI,
            MissionsCollector missionsCollector,
            IGameStateMachine gameStateMachine)
        {
            _gameplayUI = gameplayUI;
            _missionsCollector = missionsCollector;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameplayUI.QuestPanelUI.Initialize(_missionsCollector.Missions);
            _missionsCollector.OnCompleted += OnQuestsCompleted;
        }

        private void OnQuestsCompleted()
        {
            _missionsCollector.OnCompleted -= OnQuestsCompleted;
            _gameStateMachine.Enter<GameOverState>();
        }

        public void Dispose()
        {
            _gameplayUI.QuestPanelUI.Dispose();
            _missionsCollector.OnCompleted -= OnQuestsCompleted;
        }
    }
}