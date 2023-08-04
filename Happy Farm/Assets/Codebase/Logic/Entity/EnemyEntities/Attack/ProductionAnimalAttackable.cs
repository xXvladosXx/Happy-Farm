using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities.Attack
{
    public class ProductionAnimalAttackable : MonoBehaviour, IAttackable
    {
        public bool WasDamaged { get; private set; }

        public void TakeDamage() => WasDamaged = true;
    }
}