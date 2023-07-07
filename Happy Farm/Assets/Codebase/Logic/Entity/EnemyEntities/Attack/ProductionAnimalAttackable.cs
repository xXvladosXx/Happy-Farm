using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities.Attack
{
    public class ProductionAnimalAttackable : MonoBehaviour, IAttackable
    {
        private IDestroyable _productionAnimal;

        public void Construct(IDestroyable productionAnimal)
        {
            _productionAnimal = productionAnimal;
        }
        
        public void TakeDamage()
        {
            _productionAnimal.Destroy();
        }
    }
}