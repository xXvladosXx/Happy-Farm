using System;
using Codebase.Logic.QuestSystem.Core;
using Codebase.UI;
using Zenject;

namespace Codebase.Controllers
{
    public class QuestController : IInitializable, IDisposable
    {
        private readonly GameplayUI _gameplayUI;
        private readonly MissionsCollector _missionsCollector;

        public QuestController(GameplayUI gameplayUI,
            MissionsCollector missionsCollector)
        {
            _gameplayUI = gameplayUI;
            _missionsCollector = missionsCollector;
        }

        public void Initialize()
        {
            _gameplayUI.QuestPanelUI.Initialize(_missionsCollector.Missions);
        }

        public void Dispose()
        {
            
        }
    }
}