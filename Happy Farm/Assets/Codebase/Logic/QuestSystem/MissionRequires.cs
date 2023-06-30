using Codebase.Gameplay;

namespace Codebase.Logic.QuestSystem
{
    public class MissionRequires
    {
        public readonly IStorageUser StorageUser;

        public MissionRequires(IStorageUser storageUser)
        {
            StorageUser = storageUser;
        }
    }
}