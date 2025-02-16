using Cinemachine;
using UnityEngine;
using Zenject;

namespace Player.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        
        [Space, Header("Null = relative to camera")]
        [SerializeField] private Transform _moveRelativeTo;
        
        [Space, Header("Camera move boundaries")]
        [SerializeField] private Vector2 _xBounds = new(-10f, 10f);
        [SerializeField] private Vector2 _zBounds = new(-10f, 10f);
        
        private CinemachineVirtualCamera _playerCamera;
        private InputReader _input;
        private Vector2 _moveDirection;
        
        [Inject]
        private void Construct(InputReader input, CinemachineVirtualCamera playerCamera)
        {
            _input = input;
            _playerCamera = playerCamera;
        }

        private void Awake()
        {
            ValidateComponents();
            
            _input.OnMoveCamera += OnMoveCamera;

            if (!_moveRelativeTo)
            {
                _moveRelativeTo = _playerCamera.transform;
            }
        }

        private void ValidateComponents()
        {
            _input = _input ?? throw new MissingReferenceException("No installer for player inputs! Add PlayerInstaller into the scene and SceneContext");
            _playerCamera = _playerCamera ?? throw new MissingComponentException("Player camera is null! Check PlayerInstaller");
        }

        private void OnDestroy()
        {
            if(_input == null) return;
            _input.OnMoveCamera -= OnMoveCamera;
        }
        
        private void OnMoveCamera(Vector2 direction)
        {
            _moveDirection = direction;
            enabled = direction != Vector2.zero;
        }

        private void LateUpdate()
        {
            if (!_playerCamera) return;
            
            var moveVector = CalculateCameraMoveVector();
            var newPosition = ClampToBounds(_moveRelativeTo.position + moveVector);
            _playerCamera.transform.position = newPosition;
        }

        private Vector3 CalculateCameraMoveVector()
        {
            var forward = _moveRelativeTo.forward;
            var right = _moveRelativeTo.right;
            
            forward.y = 0;
            right.y = 0;
            
            forward.Normalize();
            right.Normalize();

            return (right * _moveDirection.x + forward * _moveDirection.y) * (_moveSpeed * Time.deltaTime);
        }

        private Vector3 ClampToBounds(Vector3 nextPosition)
        {
            nextPosition.x = Mathf.Clamp(nextPosition.x, _xBounds.x, _xBounds.y);
            nextPosition.z = Mathf.Clamp(nextPosition.z, _zBounds.x, _zBounds.y);
            
            return nextPosition;
        }
        
#if UNITY_EDITOR
        // Drawing an area where the camera can move
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            
            var center = new Vector3((_xBounds.x + _xBounds.y) / 2, 0, (_zBounds.x + _zBounds.y) / 2);
            var size = new Vector3(_xBounds.y - _xBounds.x, 0.1f, _zBounds.y - _zBounds.x);
            
            Gizmos.DrawWireCube(center, size);
        }
#endif
    }
}
