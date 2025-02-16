namespace Units
{
    public class EnemyUnit : UnitBase
    {
        protected override void InitializeUnitList()
        {
            UnitsManager.RegisterEnemyUnit(this);
            UnitsToChase = UnitsManager.PlayerUnits;
        }

        protected override void UnregisterUnit()
        {
            UnitsManager.UnregisterEnemyUnit(this);
        }
    }
}