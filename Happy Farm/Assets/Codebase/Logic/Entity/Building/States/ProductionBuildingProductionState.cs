using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.Building.States
{
    public class ProductionBuildingProductionState: State<ProductionConstruction>
    {
        private readonly float _productionTime;
        private float _currentTime;
        public ProductionBuildingProductionState(ProductionConstruction stateInitializer, float productionTime) : base(stateInitializer)
        {
            _productionTime = productionTime;
        }

        public override void OnEnter()
        {
            Initializer.Producer.StartProduction();
            Initializer.WasConsumed = false;
            _currentTime = 0;
        }

        public override void OnUpdate()
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= _productionTime)
                Initializer.Producer.StopProduction();
        }

        public override async void OnExit()
        {
            await Initializer.Producer.Produce(Initializer.Transform.forward);
        }
    }
}