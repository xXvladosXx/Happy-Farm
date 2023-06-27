using Codebase.Logic.Entity.StateMachine;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalStarveState : State<ProductionAnimal>
    {
        public ProductionAnimalStarveState(ProductionAnimal stateInitializer) : base(stateInitializer)
        {
        }
        
        public override void OnEnter()
        {
            Initializer.Movement.ResetSpeed();
        }

        public override void OnUpdate()
        {
            Initializer.Eater.Starve();
            Initializer.Movement.SetSpeed(0);
            Initializer.AnimatorStateReader.Tick();
        }
    }
}