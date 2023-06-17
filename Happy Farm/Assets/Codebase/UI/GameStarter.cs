using System;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        private void Start()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            Debug.Log("Start clicked");
            _gameStateMachine.Enter<LoadLevelState, string>("Gameplay");
            _startButton.onClick.RemoveListener(OnStartClicked);
        }
    }
}