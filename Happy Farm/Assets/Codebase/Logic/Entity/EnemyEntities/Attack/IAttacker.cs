using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities.Attack
{
    public interface IAttacker
    {
        void Attack(Collider collider);
    }
}