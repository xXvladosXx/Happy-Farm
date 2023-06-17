using System;
using Codebase.Utils.Input;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Camera
{
    public class CameraMotion : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _smoothing = 5f;
        [SerializeField] private Vector2 _bounds = new Vector2(10f, 10f);
        
        private Vector3 _targetPosition;
        private Vector3 _input;
        private IInputProvider _inputProvider;

        [Inject]
        public void Construct(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider; 
            _inputProvider.EnablePlayer();
        }
        
        public void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            HandleInput();
            Move();
        }

        private void Move()
        {
            Vector3 nextTargetPosition = _targetPosition + _input * _speed;
            
            if(IsInBounds(nextTargetPosition))
                _targetPosition = nextTargetPosition;
            
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _smoothing * Time.deltaTime);
        }

        private void HandleInput()
        {
            float x = _inputProvider.Axis.x;
            float z = _inputProvider.Axis.y;
            
            Vector3 right = transform.right * x;
            Vector3 forward = transform.forward * z;
            
            _input = (right + forward).normalized;
        }

        private bool IsInBounds(Vector3 nextTargetPosition)
        {
            return nextTargetPosition.x > -_bounds.x && nextTargetPosition.x < _bounds.x &&
                   nextTargetPosition.z > -_bounds.y && nextTargetPosition.z < _bounds.y;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 5);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_bounds.x*2, 5f, _bounds.y*2));
        }
#endif
    }
}