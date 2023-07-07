using Codebase.Logic.Storage.Container;

namespace Codebase.Logic.Storage
{
    public class StorageUser : IStorageUser
    {
        public IContainer Inventory { get; set; }
    }
}