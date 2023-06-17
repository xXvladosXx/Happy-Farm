using System;
using Codebase.Logic.Entity.Building.States;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class ProductionConstruction : IDisposable
    {
        [field: SerializeField] public float ProductionTime { get; private set; }

        private IConsumer _consumer;
        public IProducer Producer { get; private set; }
        public Transform Transform { get; private set; }

        public bool WasConsumed { get; set; }
        
        private EntityStateMachine<ProductionConstruction> _stateMachine;

        public ProductionConstruction(IProducer producer,
            IConsumer consumer,
            Transform transform)
        {
            Producer = producer;
            Transform = transform;
            _consumer = consumer;
            _stateMachine = new EntityStateMachine<ProductionConstruction>();

            var idleState = new ProductionBuildingIdleState(this);
            var productionState = new ProductionBuildingProductionState(this, ProductionTime);

            _stateMachine.AddStates(idleState, productionState);
        }

        public void Initialize()
        {
            _consumer.OnConsumed += OnConsumed;
        }
        
        public void Dispose()
        {
            _consumer.OnConsumed -= OnConsumed;
        }
        
        private void OnConsumed()
        {
            WasConsumed = true;
        }

        public void BindTransitions()
        {
            Func<bool> isNotProducing = () => Producer.InProduction == false;
            Func<bool> wasConsumed = () => WasConsumed;

            _stateMachine.AddTransition<ProductionBuildingIdleState, ProductionBuildingProductionState>(wasConsumed);
            _stateMachine.AddTransition<ProductionBuildingProductionState, ProductionBuildingIdleState>(isNotProducing);

            _stateMachine.SetState<ProductionBuildingIdleState>();
        }

        public void Update()
        {
            _stateMachine.Update();
        }
    }
}