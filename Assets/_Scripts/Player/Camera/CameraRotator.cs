using Cinemachine;
using UnityEngine;
using Zenject;

namespace Player.Camera
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 10f;
        private float _rotationSide;
        
        private CinemachineVirtualCamera _playerCamera;
        private InputReader _input;
        
        [Inject]
        private void Construct(InputReader input, CinemachineVirtualCamera playerCamera)
        {
            _input = input;
            _playerCamera = playerCamera;
        }

        private void Awake()
        {
            ValidateComponents();
            _input.OnRotateCamera += OnRotateCamera;
        }

        private void ValidateComponents()
        {
            _input = _input ?? throw new MissingReferenceException("No installer for player inputs! Add PlayerInstaller into the scene and SceneContext");
            _playerCamera = _playerCamera ?? throw new MissingComponentException("Player camera is null! Check PlayerInstaller");
        }

        private void OnDestroy()
        {
            if(_input == null) return;
            _input.OnRotateCamera -= OnRotateCamera;
        }
        
        private void OnRotateCamera(float rotationSide)
        {
            _rotationSide = rotationSide;
            enabled = rotationSide != 0;
        }

        private void LateUpdate()
        {
            if (!_playerCamera) return;
            
            var nextRotation = Quaternion.Euler(
                _playerCamera.transform.rotation.eulerAngles.x,
                _playerCamera.transform.rotation.eulerAngles.y + (_rotationSide * _rotateSpeed * Time.deltaTime),
                _playerCamera.transform.rotation.eulerAngles.z);
            
            _playerCamera.transform.rotation = nextRotation;
        }
    }
}
