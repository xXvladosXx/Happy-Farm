using System;
using System.Linq;
using Codebase.Logic.Entity.Movement;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.States;
using Codebase.Logic.Entity.StateMachine;
using Codebase.Utils.Transform;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities
{
    public class ProductionAnimal
    {
        public Transform Transform { get; set; }
        
        public readonly IEater Eater;
        public readonly IMovable Movement;
        public readonly IProducer Producer;
        
        private readonly EatableRegistry _eatableRegistry;

        private readonly EntityStateMachine<ProductionAnimal> _stateMachine;

        private bool _isHungry;
        public ProductionAnimal(IEater eater,
            IMovable movement,
            Transform transform,
            EatableRegistry eatableRegistry, 
            IProducer producer)
        {
            Transform = transform;
            Eater = eater;
            Movement = movement;
            Producer = producer;
            
            _eatableRegistry = eatableRegistry;

            _stateMachine = new EntityStateMachine<ProductionAnimal>();
            
            var idleState = new ProductionAnimalIdleState(this);
            var deathState = new ProductionAnimalDeathState(this);
            var eatState = new ProductionAnimalEatState(this, _eatableRegistry);
            var walkState = new ProductionAnimalWalkState(this);
            var starveState = new ProductionAnimalStarveState(this);
            
            _stateMachine.AddStates(idleState, deathState,eatState, walkState, starveState);
        }

        public void BindTransitions()
        {
            Func<bool> hasFoodToEat = () => _eatableRegistry.Eatables.Count > 0 && _eatableRegistry.Eatables.Count(x => !x.Equals(null)) > 0;
            Func<bool> isHungry = () => _isHungry;
            Func<bool> isNearTarget = () => Transform.IsNear(Movement.Target, 2);
            Func<bool> hasTarget = () => Movement.Target != Vector3.zero;
            Func<bool> healthLessThanZero = () => Eater.Health.CurrentHealth <= 0;

            _stateMachine.AddTransition<ProductionAnimalIdleState, ProductionAnimalWalkState>(hasTarget);
            _stateMachine.AddTransition<ProductionAnimalWalkState, ProductionAnimalIdleState>(() => isNearTarget() && !isHungry());
            
            _stateMachine.AddTransition<ProductionAnimalWalkState, ProductionAnimalStarveState>(() => !hasFoodToEat() && isHungry());
            _stateMachine.AddTransition<ProductionAnimalStarveState, ProductionAnimalWalkState>(() => hasFoodToEat() && isHungry());
            
            _stateMachine.AddTransition<ProductionAnimalEatState, ProductionAnimalWalkState>(() => !isHungry() || !hasFoodToEat());
            _stateMachine.AddTransition<ProductionAnimalWalkState, ProductionAnimalEatState>(() => hasFoodToEat() && isHungry());
            
            _stateMachine.AddTransition<ProductionAnimalStarveState, ProductionAnimalDeathState>(healthLessThanZero);
            
            _stateMachine.SetState<ProductionAnimalIdleState>();
        }

        public void Initialize()
        {
            Eater.OnHunger += SetHungry;
            Eater.OnAte += SetFull;
        }

        public void Dispose()
        {
            Eater.OnHunger -= SetHungry;
            Eater.OnAte -= SetFull;
        }
        
        public void Update() => 
            _stateMachine.Update();

        private void SetFull() => 
            _isHungry = false;

        private void SetHungry() => 
            _isHungry = true;
    }
}