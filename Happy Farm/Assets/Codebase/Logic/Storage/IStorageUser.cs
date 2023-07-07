using Codebase.Logic.Storage.Container;

namespace Codebase.Logic.Storage
{
    public interface IStorageUser
    {
        IContainer Inventory { get; set; }
    }
}