using UnityEngine;
using Zenject;

using Player;
using Spawners.Factories;

namespace Spawners
{
    public class PlayerUnitsSpawner : MonoBehaviour
    {
        private PlayerUnitsFactory _playerUnitsFactory;
        private Camera _playerCamera;
        private InputReader _input;

        [Inject]
        private void Construct(InputReader input, Camera playerCamera, PlayerUnitsFactory playerUnitsFactory)
        {
            _input = input;
            _playerCamera = playerCamera;

            _playerUnitsFactory = playerUnitsFactory;
        }

        private void Start()
        {
            ValidateComponents();
            _input.OnChangeUnitsPosition += Spawn;
        }

        private void ValidateComponents()
        {
            _input = _input ??
                     throw new MissingComponentException("No input reader found! Add PlayerInstaller into the scene");
            _playerCamera = _playerCamera ??
                            throw new MissingComponentException(
                                "No CinemachineVirtual found! Add Camera into the player installer");
        }

        private void OnDestroy()
        {
            if(_input == null) return;
            _input.OnChangeUnitsPosition -= Spawn;
        }

        private void Spawn()
        {
            var ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var unit = _playerUnitsFactory.Get(hit.point, Quaternion.identity);
                unit.Initialize();
            }
        }
    }
}