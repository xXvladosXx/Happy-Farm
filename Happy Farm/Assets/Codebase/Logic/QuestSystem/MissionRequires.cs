using Codebase.Logic.Entity.Building;
using Codebase.Logic.Storage;

namespace Codebase.Logic.QuestSystem
{
    public class MissionRequires
    {
        public readonly IStorageUser StorageUser;
        public readonly BuildingRegistry BuildingRegistry;

        public MissionRequires(IStorageUser storageUser, 
            BuildingRegistry buildingRegistry)
        {
            StorageUser = storageUser;
            BuildingRegistry = buildingRegistry;
        }
    }
}