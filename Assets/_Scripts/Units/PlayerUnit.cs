using Spawners.Factories;
using UnityEngine;
using Zenject;

namespace Units
{
    public class PlayerUnit : UnitBase
    {
        [SerializeField] private float _touchingEachOtherDistance = 1.7f;

        private EnemyUnitsFactory _enemyUnitsFactory;
        private PlayerUnitsFactory _playerUnitsFactory;

        [Inject]
        private void Construct(EnemyUnitsFactory enemyUnitsFactory, PlayerUnitsFactory playerUnitsFactory)
        {
            _enemyUnitsFactory = enemyUnitsFactory;
            _playerUnitsFactory = playerUnitsFactory;
        }
        
        protected override void InitializeUnitList()
        {
            UnitsManager.RegisterPlayerUnit(this);
            UnitsToChase = UnitsManager.EnemyUnits;
        }

        protected override void UnregisterUnit()
        {
            UnitsManager.UnregisterPlayerUnit(this);
        }

        protected override void Update()
        {
            // As OnCollision/OnTrigger requires physics (rigidbody + collider),
            // I decided to 'calculate' colliding using Distance between PlayerUnit and EnemyUnit
            HandleColliding();
            base.Update();
        }

        // For better optimization, we don't need to handle colliding
        // in both (enemy and player)
        private void HandleColliding()
        {
            if (!Target) return;
            if (Vector3.Distance(transform.position, Target.position) <= _touchingEachOtherDistance)
            {
                ReturnUnitsToPool();
            }
        }

        private void ReturnUnitsToPool()
        {
            if (Target.TryGetComponent<EnemyUnit>(out var enemyUnit))
            {
                _enemyUnitsFactory.Release(enemyUnit);
                _playerUnitsFactory.Release(this);
            }
        }
    }
}