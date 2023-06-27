using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.States
{
    public class ProductionBuildingProductionState: State<ProductionConstruction>
    {
        private float _currentTime;
        public ProductionBuildingProductionState(ProductionConstruction stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.WasConsumed = false;
            _currentTime = 0;
        }

        public override void OnUpdate()
        {
           
        }
    }
}