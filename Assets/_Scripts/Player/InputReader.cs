using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class InputReader : IInitializable, IDisposable
    {
        public event Action OnChangeUnitsPosition;
        
        public event Action<Vector2> OnMoveCamera;
        public event Action<float> OnRotateCamera;

        private PlayerInputMap _playerInputMap;
        
        public void Initialize()
        {
            _playerInputMap = new PlayerInputMap();
            
            SubscribeInputs();
            _playerInputMap.Player.Enable();
        }

        private void SubscribeInputs()
        {
            _playerInputMap.Player.ChangeUnitsPosition.performed += OnNotifyChangeUnitsPosition;

            _playerInputMap.Player.MoveCamera.performed += OnNotifyCameraMove;
            _playerInputMap.Player.MoveCamera.canceled += OnNotifyCameraMove;

            _playerInputMap.Player.RotateCamera.performed += OnNotifyCameraRotate;
            _playerInputMap.Player.RotateCamera.canceled += OnNotifyCameraRotate;
        }

        public void Dispose()
        {
            _playerInputMap.Player.Disable();
            
            UnsubscribeInputs();
            ClearActions();
            
            _playerInputMap.Dispose();
        }
        
        private void UnsubscribeInputs()
        {
            _playerInputMap.Player.ChangeUnitsPosition.performed -= OnNotifyChangeUnitsPosition;
            
            _playerInputMap.Player.MoveCamera.performed -= OnNotifyCameraMove;
            _playerInputMap.Player.MoveCamera.canceled -= OnNotifyCameraMove;
            
            _playerInputMap.Player.RotateCamera.performed -= OnNotifyCameraRotate;
            _playerInputMap.Player.RotateCamera.canceled -= OnNotifyCameraRotate;
        }
        
        private void ClearActions()
        {
            OnChangeUnitsPosition = null;
            OnMoveCamera = null;
            OnRotateCamera = null;
        }

        private void OnNotifyChangeUnitsPosition(InputAction.CallbackContext context)
        {
            OnChangeUnitsPosition?.Invoke();
        }
        
        private void OnNotifyCameraMove(InputAction.CallbackContext context)
        {
            OnMoveCamera?.Invoke(context.ReadValue<Vector2>());
        }
        
        private void OnNotifyCameraRotate(InputAction.CallbackContext context)
        {
            OnRotateCamera?.Invoke(context.ReadValue<float>());
        }
    }
}
