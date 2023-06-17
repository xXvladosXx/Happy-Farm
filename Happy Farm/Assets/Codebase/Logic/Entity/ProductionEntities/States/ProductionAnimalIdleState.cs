using Codebase.Logic.Entity.StateMachine;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalIdleState : State<ProductionAnimal>
    {
        public ProductionAnimalIdleState(ProductionAnimal productionAnimal) : base(productionAnimal)
        {
            
        }
    }
}