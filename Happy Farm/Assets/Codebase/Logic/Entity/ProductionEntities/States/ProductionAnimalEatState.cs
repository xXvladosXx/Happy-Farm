using System.Linq;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalEatState : State<ProductionAnimal>
    {
        public ProductionAnimalEatState(ProductionAnimal stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.AnimatorStateReader.PlayAnimation(Initializer.AnimatorStateReader.AnimatorStateHasher.InteractHash, true);
        }

        public override void OnUpdate()
        {
            Initializer.Movement.SetSpeed(0);
            Initializer.AnimatorStateReader.Tick();
            Initializer.Eater.Eat();
        }

        public override void OnExit()
        {
            Initializer.AnimatorStateReader.PlayAnimation(Initializer.AnimatorStateReader.AnimatorStateHasher.InteractHash, false);
        }
    }
}