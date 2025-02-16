using System;
using Spawners.Factories.Interfaces;
using Units;
using UnityEngine;

namespace Spawners.Factories
{
    public class EnemyUnitsFactory : Base.Factory<EnemyUnit>, INotifyOnUnitSpawned, INotifyOnUnitDied, IDisposable
    {
        public event Action<int> OnUnitSpawned;
        public event Action<int> OnUnitDied;

        public EnemyUnitsFactory(EnemyUnit prefab) : 
            base(prefab) { }

        public override void Release(EnemyUnit instance)
        {
            base.Release(instance);
            OnUnitDied?.Invoke(ActivePoolCapacity);
        }

        public override EnemyUnit Get(Vector3 position, Quaternion rotation)
        {
            // Retrieve a unit from the pool to ensure it's created or reused before updating observers
            var unit = base.Get(position, rotation);

            // Notify observers with the updated active and correct enemy count
            OnUnitSpawned?.Invoke(ActivePoolCapacity);
    
            return unit;
        }

        // Bound by Zenject (FactoryInstaller)
        public void Dispose()
        {
            OnUnitDied = null;
            OnUnitSpawned = null;
        }
    }
}