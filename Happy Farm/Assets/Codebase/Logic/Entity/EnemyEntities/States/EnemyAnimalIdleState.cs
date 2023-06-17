using Codebase.Logic.Entity.StateMachine;
using Codebase.Logic.Storage.Container;

namespace Codebase.Logic.Entity.EnemyEntities.States
{
    public class EnemyAnimalIdleState : State<EnemyAnimal>
    {
        public EnemyAnimalIdleState(EnemyAnimal stateInitializer) : base(stateInitializer)
        {
        }

        public override void OnEnter()
        {
            Initializer.Collectable.CanBeCollected = false;
        }

        public override void OnUpdate()
        {
            Initializer.AnimatorStateReader.Tick();
        }
    }
}