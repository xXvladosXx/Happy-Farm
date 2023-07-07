using System;
using Codebase.Logic.Entity.Building.States;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.Production.Producers;
using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class ProductionConstruction 
    {
        public IProducer Producer { get; private set; }
        public Transform Transform { get; private set; }

        private EntityStateMachine<ProductionConstruction> _stateMachine;

        public ProductionConstruction(IProducer producer,
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

            _stateMachine.AddTransition<ProductionBuildingIdleState, ProductionBuildingProductionState>(() => !isNotProducing());
            _stateMachine.AddTransition<ProductionBuildingProductionState, ProductionBuildingIdleState>(isNotProducing);

            _stateMachine.SetState<ProductionBuildingIdleState>();
        }

        public void Update()
        {
            _stateMachine.Update();
        }
    }
}