using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Storage.Container;

namespace Codebase.Logic.Entity.EnemyEntities.States
{
    public class EnemyAnimalCaughtState : State<EnemyAnimal>
    {
        public EnemyAnimalCaughtState(EnemyAnimal stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.Collectable.CanBeCollected = true;
            Initializer.Movement.ResetSpeed();
        }
    }
}