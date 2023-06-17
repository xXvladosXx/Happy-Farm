using System;
using Codebase.Utils.Input;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Camera
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _smoothing = 5;
        
        private float _targetAngle;
        private float _currentAngle;
        private IInputProvider _inputProvider;

        [Inject]
        public void Construct(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }
        
        private void Awake()
        {
            _targetAngle = transform.eulerAngles.y;
            _currentAngle = _targetAngle;
        }

        private void Update()
        {
            HandleInput();
            Rotate();
        }

        private void HandleInput()
        {
            if(!_inputProvider.IsRightButtonUp())
                return;
            
            _targetAngle += _inputProvider.MouseAxis * _rotationSpeed;
        }

        private void Rotate()
        {
           _currentAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, _smoothing * Time.deltaTime);
           transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.up);
        }
    }
}