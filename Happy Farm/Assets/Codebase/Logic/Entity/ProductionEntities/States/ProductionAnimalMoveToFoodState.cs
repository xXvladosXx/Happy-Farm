using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalMoveToFoodState: State<ProductionAnimal>
    {
        public ProductionAnimalMoveToFoodState(ProductionAnimal productionAnimal) : base(productionAnimal)
        {
            
        }

        public override void OnEnter()
        {
            FindRandomEatable();
            Initializer.Movement.Move(Initializer.Eater.Eatable.Transform.position);
        }

        public override void OnUpdate()
        {
            Initializer.Movement.SetSpeed(Initializer.Movement.RunSpeed);
            Initializer.AnimatorStateReader.Tick();
        }

        private void FindRandomEatable()
        {
            var random = Random.Range(0, Initializer.EatableRegistry.Eatables.Count);
            Initializer.Eater.Eatable = Initializer.EatableRegistry.Eatables[random];
        }
    }
}