using System.Linq;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.StateMachine;
using UnityEngine;

namespace Codebase.Logic.Entity.ProductionEntities.States
{
    public class ProductionAnimalEatState : State<ProductionAnimal>
    {
        private readonly EatableRegistry _eatableRegistry;
        private IEatable _eatable;

        public ProductionAnimalEatState(ProductionAnimal stateInitializer, EatableRegistry eatableRegistry) : base(stateInitializer)
        {
            _eatableRegistry = eatableRegistry;
        }

        public override void OnEnter()
        {
            Initializer.Movement.SetSpeed(6);

            FindRandomEatable();

            Initializer.Movement.Move(_eatable.Transform.position);
        }

        public override void OnUpdate()
        {
            if (_eatable == null || _eatable.Equals(null))
            {
                FindRandomEatable();
                if(_eatable == null)
                    return;
                
                Initializer.Movement.Move(_eatable.Transform.position);
            }
            
            Initializer.Eater.Eat(_eatable);
        }

        public override async void OnExit()
        {
            await Initializer.Producer.Produce(Initializer.Transform.position);
        }

        private void FindRandomEatable()
        {
            var random = Random.Range(0, _eatableRegistry.Eatables.Count);
            _eatable = _eatableRegistry.Eatables[random];
        }
    }
}