using Cinemachine;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    [DisallowMultipleComponent]
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineVirtualCamera _playerCinemachine;
        [SerializeField] private Camera _playerCamera;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputReader>()
                .AsSingle()
                .NonLazy();

            Container.Bind<CinemachineVirtualCamera>()
                .FromInstance(_playerCinemachine)
                .AsSingle();

            Container.Bind<Camera>()
                .FromInstance(_playerCamera)
                .AsSingle();
        }
    }
}