using System;

namespace Spawners.Factories.Interfaces
{
    public interface INotifyOnUnitDied
    {
        /// <summary>
        /// Gives current count of alive units, when unit releases in pool
        /// </summary>
        public event Action<int> OnUnitDied;
    }
}