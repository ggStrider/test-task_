using System;

namespace Spawners.Factories.Interfaces
{
    public interface INotifyOnUnitSpawned
    {
        /// <summary>
        /// Gives current count of alive units, when unit is getting from pool
        /// </summary>
        public event Action<int> OnUnitSpawned;
    }
}