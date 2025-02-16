using UnityEngine;
using Player;
using Spawners.Factories;
using Units;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [Header("Enemy factory")]
        [SerializeField] private EnemyUnit _enemyUnitPrefab;
        
        [Space] [Header("Player factory")]
        [SerializeField] private PlayerUnit _playerUnitPrefab;
        
        [Space] [Header("Both")]
        [SerializeField] private UnitsManager _unitsManager; 

        public override void InstallBindings()
        {
            /* For both */
            Container.BindInterfacesAndSelfTo<UnitsManager>()
                .FromScriptableObject(_unitsManager)
                .AsSingle();
            
            /* Enemy factory */
            Container.BindInterfacesAndSelfTo<EnemyUnitsFactory>()
                .AsSingle();
            
            Container.Bind<EnemyUnit>()
                .FromInstance(_enemyUnitPrefab)
                .AsSingle();
            
            /* Player factory */
            Container.BindInterfacesAndSelfTo<PlayerUnitsFactory>()
                .AsSingle();
            
            Container.Bind<PlayerUnit>()
                .FromInstance(_playerUnitPrefab)
                .AsSingle();
        }
    }
}