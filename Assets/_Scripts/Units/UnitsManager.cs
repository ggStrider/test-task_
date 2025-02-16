using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Units
{
    // I can't think of a better option
    // that has more optimization and simplicity than this one
    [CreateAssetMenu(fileName = "Units Manager", menuName = "_Managers/Units Manager")]
    public class UnitsManager : ScriptableObject, IDisposable
    {
        [HideInInspector] public List<UnitBase> PlayerUnits = new();
        [HideInInspector] public List<UnitBase> EnemyUnits = new();

        public void RegisterPlayerUnit(PlayerUnit unit)
        {
            if(PlayerUnits.Contains(unit)) return;
            PlayerUnits.Add(unit);
        }

        public void UnregisterPlayerUnit(PlayerUnit unit)
        {
            if(!PlayerUnits.Contains(unit)) return;
            PlayerUnits.Remove(unit);
        }
        
        public void RegisterEnemyUnit(EnemyUnit unit)
        {
            if(EnemyUnits.Contains(unit)) return;
            EnemyUnits.Add(unit);
        }

        public void UnregisterEnemyUnit(EnemyUnit unit)
        {
            if(!EnemyUnits.Contains(unit)) return;
            EnemyUnits.Remove(unit);
        }

        // Bound by Zenject (FactoryInstaller)
        public void Dispose()
        {
            PlayerUnits.Clear();
            EnemyUnits.Clear();
        }
    }
}