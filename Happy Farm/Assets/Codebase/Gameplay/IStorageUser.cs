using Codebase.Logic.Storage.Container;

namespace Codebase.Gameplay
{
    public interface IStorageUser
    {
        IContainer Inventory { get; set; }
    }
}