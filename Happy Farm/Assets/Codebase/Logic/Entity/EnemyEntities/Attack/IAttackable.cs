namespace Codebase.Logic.Entity.EnemyEntities.Attack
{
    public interface IAttackable
    {
        void TakeDamage();
        bool WasDamaged { get; }
    }
}