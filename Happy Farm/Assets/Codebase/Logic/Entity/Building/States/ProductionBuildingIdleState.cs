using Codebase.Logic.Entity.StateMachine;

namespace Codebase.Logic.Entity.Building.States
{
    public class ProductionBuildingIdleState : State<ProductionConstruction>
    {
        public ProductionBuildingIdleState(ProductionConstruction stateInitializer) : base(stateInitializer)
        {
        }
    }
}