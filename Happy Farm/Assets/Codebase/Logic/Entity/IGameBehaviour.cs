using Codebase.Logic.Stats;
using Sirenix.OdinInspector;

namespace Codebase.Logic.Entity
{
    public interface IGameBehaviour
    {
        public bool GameUpdate();
        public void Recycle();
    }
}