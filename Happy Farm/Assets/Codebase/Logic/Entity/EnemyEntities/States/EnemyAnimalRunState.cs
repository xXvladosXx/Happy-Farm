using Codebase.Logic.Entity.StateMachine;

namespace Codebase.Logic.Entity.EnemyEntities.States
{
    public class EnemyAnimalRunState : State<EnemyAnimal>
    {
        public EnemyAnimalRunState(EnemyAnimal stateInitializer) : base(stateInitializer)
        {
        }
    }
}