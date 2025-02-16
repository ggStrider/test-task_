using System;
using Player;
using Spawners.Factories.Interfaces;
using Units;
using UnityEngine;

namespace Spawners.Factories
{
    public class PlayerUnitsFactory : Base.Factory<PlayerUnit>, INotifyOnUnitSpawned, INotifyOnUnitDied, IDisposable
    {
        public event Action<int> OnUnitSpawned;
        public event Action<int> OnUnitDied;

        public PlayerUnitsFactory(PlayerUnit prefab) : 
            base(prefab) { }

        public override void Release(PlayerUnit instance)
        {
            base.Release(instance);
            OnUnitDied?.Invoke(ActivePoolCapacity);
        }

        public override PlayerUnit Get(Vector3 position, Quaternion rotation)
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