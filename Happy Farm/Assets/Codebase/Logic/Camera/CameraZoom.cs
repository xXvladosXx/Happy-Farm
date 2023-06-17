using System;
using Codebase.Utils.Input;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _smoothing = 5f;
        [SerializeField] private Vector2 _bounds = new Vector2(10f, 70f);
        [SerializeField] private Transform _cameraHolder;
        
        private Vector3 _targetPosition;
        private float _input;
        private IInputProvider _inputProvider;

        private Vector3 CameraDirection => transform.InverseTransformDirection(_cameraHolder.forward);

        [Inject]
        public void Construct(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }
        
        private void Awake()
        {
            _targetPosition = _cameraHolder.localPosition;
        }
        
        private void Update()
        {
            HandleInput();
            Zoom();
        }

        private void HandleInput()
        {
            _input = _inputProvider.ScrollAxis;
        }
        
        private void Zoom() {
            Vector3 nextTargetPosition = _targetPosition + CameraDirection * (_input * _speed);
            if(IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;
            _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * _smoothing);
        }

        private bool IsInBounds(Vector3 position) {
            return position.magnitude > _bounds.x && position.magnitude < _bounds.y;
        }
    }
}