using System;
using Codebase.Logic.Entity.Building.States;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.Production.Producers;
using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Stats;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class ProductionConstruction : Construction 
    {
        public TimeableProducer Producer { get; private set; }
        public Transform Transform { get; private set; }

        private EntityStateMachine<ProductionConstruction> _stateMachine;

        public ProductionConstruction(TimeableProducer producer,
            Transform transform)
        {
            Producer = producer;
            Transform = transform;

            _stateMachine = new EntityStateMachine<ProductionConstruction>();

            var idleState = new ProductionBuildingIdleState(this);
            var productionState = new ProductionBuildingProductionState(this);

            _stateMachine.AddStates(idleState, productionState);
        }

        public void BindTransitions()
        {
            Func<bool> isNotProducing = () => Producer.InProduction == false;
            Func<bool> isProducing = () => Producer.InProduction;

            _stateMachine.AddTransition<ProductionBuildingIdleState, ProductionBuildingProductionState>(isProducing);
            _stateMachine.AddTransition<ProductionBuildingProductionState, ProductionBuildingIdleState>(isNotProducing);

            _stateMachine.SetState<ProductionBuildingIdleState>();
        }

        public override void Update()
        {
            _stateMachine.Update();
        }
    }
}