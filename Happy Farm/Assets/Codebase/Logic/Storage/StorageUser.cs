using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.Storage.Container;

namespace Codebase.Logic.Storage
{
    public class StorageUser : IStorageUser
    {
        public IContainer Inventory { get; private set; }

        public StorageUser(IContainer inventory)
        {
            Inventory = inventory;
        }
    }
}