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
            Initializer.Movement.SetSpeed(6);
        }

        public override void OnUpdate()
        {
            Initializer.Eater.Starve();
        }
    }
}