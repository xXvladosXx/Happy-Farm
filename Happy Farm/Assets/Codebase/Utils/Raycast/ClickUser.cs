﻿using System;
using Codebase.Utils.Input;
using Codebase.Utils.Raycast;
using UnityEngine.InputSystem;
using Zenject;

namespace Codebase.Logic.Entity.EnemyEntities.Catch
{
    public class ClickUser : IClicker, IInitializable, IDisposable
    {
        private readonly IRaycastUser _raycastUser;
        private readonly IInputProvider _inputProvider;

        public ClickUser(IRaycastUser raycastUser, 
            IInputProvider inputProvider)
        {
            _raycastUser = raycastUser;
            _inputProvider = inputProvider;
        }

        public void Initialize()
        {
            _inputProvider.PlayerActions.LeftClick.performed += OnClickPerformed;
        }

        public void Dispose()
        {
            _inputProvider.PlayerActions.LeftClick.performed -= OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext obj)
        {
            Click();
        }

        public void Click()
        {
            if(!_raycastUser.RaycastHit.HasValue)
                return;

            var clickables = _raycastUser.RaycastHit.Value.collider.GetComponents<IClickable>();
            foreach (var clickable in clickables)
            {
                clickable.Interact();
            }
        }
    }
}