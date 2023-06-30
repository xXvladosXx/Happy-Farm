using Codebase.Logic.Storage.Container;

namespace Codebase.Gameplay
{
    public class StorageUser : IStorageUser
    {
        public IContainer Inventory { get; set; }
    }
}