using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Utils.Input;
using Codebase.Utils.Raycast;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Codebase.Logic
{
    public class EatableSpawner : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private IRaycastUser _raycastUser;
        private IInputProvider _inputProvider;

        [Inject]
        public void Construct(IGameFactory gameFactory,
            IRaycastUser raycastUser,
            IInputProvider inputProvider)
        {
            _gameFactory = gameFactory;
            _raycastUser = raycastUser;
            _inputProvider = inputProvider;
        }

        private void OnEnable()
        {
            _inputProvider.PlayerActions.LeftClick.performed += CreateFood;
        }

        private void OnDisable()
        {
            _inputProvider.PlayerActions.LeftClick.performed -= CreateFood;
        }

        private async void CreateFood(InputAction.CallbackContext obj)
        {
            if(!_raycastUser.RaycastHit.HasValue)
                return;
            
            if(_raycastUser.RaycastHit.Value.transform.gameObject.layer != LayerMask.NameToLayer("Ground"))
                return;
            
            await CreateFood(_raycastUser.RaycastHit.Value.point);
        }

        private void Update()
        {
            _raycastUser.Tick();
        }

        [Button]
        public async UniTask CreateFood(Vector3 position)
        {
            await _gameFactory.CreateFood("Food", position);
        }
    }
}