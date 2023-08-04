using Codebase.Logic.Entity;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Storage;

namespace Codebase.Logic.QuestSystem
{
    public class MissionRequires
    {
        public readonly IStorageUser StorageUser;
        public readonly GameBehaviourHandler GameBehaviour;

        public MissionRequires(IStorageUser storageUser, 
            GameBehaviourHandler gameBehaviour)
        {
            StorageUser = storageUser;
            GameBehaviour = gameBehaviour;
        }
    }
}