using System.Collections.Generic;
using ProjectDawn.Navigation.Hybrid;
using Units.OnSpawn;
using UnityEngine;
using Zenject;

namespace Units
{
    public abstract class UnitBase : MonoBehaviour, ISpawnable
    {
        [SerializeField] private bool _checkNearestAlways = true;
        [SerializeField] private AgentAuthoring _agent;
        
        protected Transform Target;
        protected List<UnitBase> UnitsToChase;
        protected UnitsManager UnitsManager;

        protected abstract void InitializeUnitList();
        protected abstract void UnregisterUnit();

        [Inject]
        private void Construct(UnitsManager unitsManager)
        {
            UnitsManager = unitsManager;
            InitializeUnitList();
        }

        private void OnDestroy()
        {
            UnregisterUnit();
        }
        
        public virtual void Initialize()
        {
            Target = FindNearestUnitToChase();
            InvokeAllOnSpawnComponents();
        }

        public virtual void OnReleaseToPool()
        {
            
        }

        private void InvokeAllOnSpawnComponents()
        {
            var onSpawns = GetComponents<IOnThisUnitSpawn>();
            if(onSpawns.Length == 0) return;

            foreach (var component in onSpawns)
            {
                component.OnThisUnitSpawn();
            }
        }

        protected virtual void Update()
        {
            if (_checkNearestAlways) Target = FindNearestUnitToChase();
            else if (!Target && !_checkNearestAlways) Target = FindNearestUnitToChase();

            if (Target)
            {
                _agent.SetDestination(Target.position);
            }
            else if (!Target)
            {
                _agent.SetDestination(transform.position);
            }
        }

        protected Transform FindNearestUnitToChase()
        {
            if (UnitsToChase.Count == 0) return null;

            Transform nearestUnit = null;
            var minDistance = float.MaxValue;
            var unitPosition = _agent.transform.position;

            foreach (var unit in UnitsToChase)
            {
                if (!unit.gameObject.activeInHierarchy) continue;

                var distance = Vector3.SqrMagnitude(unit.transform.position - unitPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestUnit = unit.transform;
                }
            }

            return nearestUnit;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_agent == null) _agent = GetComponent<AgentAuthoring>();
        }
#endif
    }
}
