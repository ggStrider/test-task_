using Units;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Spawners.Factories.Base
{
    public abstract class Factory<T> where T: MonoBehaviour, ISpawnable
    {
        protected readonly T Prefab;
     
        private readonly ObjectPool<T> _pool;
        [Inject] private DiContainer _container;

        public Factory(T prefab, int defaultPoolCapacity = 1500, int maxSize = 10000)
        {
            Prefab = prefab;
            
            _pool = new ObjectPool<T>(
                Create,
                instance => instance.gameObject.SetActive(true),
                instance => instance.gameObject.SetActive(false),
                Object.Destroy,
                false,
                defaultPoolCapacity,
                maxSize);
        }

        public virtual void Release(T instance)
        {
            _pool.Release(instance);
        }

        public virtual T Get(Vector3 position, Quaternion rotation)
        {
            T instance = _pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            
            return instance;
        }

        public virtual T Create()
        {
            T instance = _container.InstantiatePrefabForComponent<T>(Prefab);
            instance.Initialize();
            
            return instance;
        }
        
        public int ActivePoolCapacity => _pool.CountActive;
        public int InactivePoolCapacity => _pool.CountInactive;
    }
}