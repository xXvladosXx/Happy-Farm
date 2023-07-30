using System;
using Codebase.Logic.ShopSystem;
using Codebase.Logic.Storage;
using Codebase.UI;
using Codebase.UI.Inventory;
using Zenject;

namespace Codebase.Controllers
{
    public class InventoryController : IInitializable, IDisposable
    {
        private readonly IStorageUser _storageUser;
        private readonly GameplayUI _gameplayUI;

        public InventoryController(IStorageUser storageUser,
            IShop shop,
            GameplayUI gameplayUI)
        {
            _storageUser = storageUser;
            _gameplayUI = gameplayUI;
            
            _gameplayUI.InventoryUI.Construct(_storageUser.Inventory, shop);
        }

        public void Initialize()
        {
            _gameplayUI.InventoryUI.Initialize();   
        }

        public void Dispose()
        {
            _gameplayUI.InventoryUI.Dispose();
        }
    }
}