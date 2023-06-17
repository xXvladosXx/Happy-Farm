using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalDeathState : State<ProductionAnimal>
    {
        public ProductionAnimalDeathState(ProductionAnimal stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.Dispose();
        }
    }
}