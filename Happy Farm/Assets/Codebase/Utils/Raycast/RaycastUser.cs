using Codebase.Utils.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Codebase.Utils.Raycast
{
    public class RaycastUser : IRaycastUser, ITickable
    {
        private readonly Camera _camera;
        private readonly IInputProvider _inputProvider;
        private readonly RaycastSettings _raycastSettings;

        public RaycastHit? RaycastHit { get; private set; }

        public RaycastUser(IInputProvider inputProvider,
            RaycastSettings raycastSettings)
        {
            _camera = Camera.main;
            _inputProvider = inputProvider;
            _raycastSettings = raycastSettings;
        }
        
        public void Tick()
        {
            RaycastHit = MakeRaycast(_inputProvider.ReadMousePosition(), _raycastSettings.MasksToRaycast);
        }

        public RaycastHit? MakeRaycast(Vector2 pointFrom, LayerMask layerMask)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return null;
            
            var ray = _camera.ScreenPointToRay(pointFrom);
            var hasHit = Physics.Raycast(ray, out var raycastHit, Mathf.Infinity, layerMask);

            if (hasHit)
                return raycastHit;

            return null;
        }
    }
}