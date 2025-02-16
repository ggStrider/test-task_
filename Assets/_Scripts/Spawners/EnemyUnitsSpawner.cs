using Model;
using Spawners.Factories;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class EnemyUnitsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        [Space] 
        [SerializeField, Min(0)] private int _maxEnemyUnits;
        [SerializeField, Min(0.01f)] private float _spawnInterval;
        [SerializeField, Min(0)] private int _spawnCountPerInterval;

        [Space]
        [Min(0)] private int _spawnOnStart;

        private EnemyUnitsFactory _unitsFactory;

        [Inject]
        private void Construct(EnemyUnitsFactory unitsFactory)
        {
            _unitsFactory = unitsFactory;
        }

        private void Start()
        {
            InitializeValuesBySettings();
            
            // Spawning start enemies count
            var calculateSpawnCount = CalculateHowMuchCanSpawn(_spawnOnStart);
            Spawn(calculateSpawnCount);
            
            // Spawning with delay
            InvokeRepeating(nameof(SpawnWithInterval), _spawnInterval, _spawnInterval);
        }

        private void InitializeValuesBySettings()
        {
            var session = FindObjectOfType<GameSession>();
            if (!session)
            {
                Debug.LogWarning("No GameSession found in scene!");
                return;
            }

            if (session.Data.DesiredEnemyUnitsOnStart >= 0)
            {
                _spawnOnStart = session.Data.DesiredEnemyUnitsOnStart;
                if (_maxEnemyUnits < _spawnOnStart) _maxEnemyUnits = _spawnOnStart;
            }
        }

        private int CalculateHowMuchCanSpawn(int desiredValue)
        {
            var availableSlots = _maxEnemyUnits - _unitsFactory.ActivePoolCapacity;
            return Mathf.Min(desiredValue, availableSlots);
        }

        private void SpawnWithInterval()
        {
            var countToSpawn = CalculateHowMuchCanSpawn(_spawnCountPerInterval);
            Spawn(countToSpawn);
        }

        private void Spawn(int howMuch)
        {
            if (howMuch <= 0) return;
            
            for (var i = 1; i <= howMuch; i++)
            {
                var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                var unit = _unitsFactory.Get(spawnPoint.position, Quaternion.identity);
                
                unit.Initialize();
            }
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_spawnOnStart > _maxEnemyUnits) _maxEnemyUnits = _spawnOnStart;
        }
        #endif
    }
}
