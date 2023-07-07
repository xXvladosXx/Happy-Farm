using Codebase.Infrastructure;
using Codebase.Infrastructure.AssetService;
using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Storage;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Input;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        public override void InstallBindings()
        {
            RegisterAssetProvider();
            RegisterStaticData();
            
            Container
                .Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefab(_gameBootstrapper);

            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefab(_coroutineRunner)
                .AsSingle();

            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            
            CreateInventory();
            CreateRegistries();
        }

        private void CreateRegistries()
        {
            Container.BindInterfacesAndSelfTo<BuildingRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<EatableRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalRegistry>().AsSingle();
        }

        private void CreateInventory()
        {
            var storageUser = new StorageUser
            {
                Inventory = new ItemContainer(0)
            };
            
            Container.Bind<IStorageUser>().To<StorageUser>().FromInstance(storageUser).AsSingle();
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadBuildings();
            staticDataService.LoadStorages();
            staticDataService.LoadProductionAnimals();
            staticDataService.LoadEnemyAnimals();
            staticDataService.LoadLevels();
            staticDataService.LoadProducts();
            staticDataService.LoadSpawnPlaces();
            staticDataService.LoadFoodProductions();
            Container.BindInstance(staticDataService).AsSingle();
        }

        private void RegisterAssetProvider()
        {
            IAssetProvider assetProvider = new AssetProvider();
            assetProvider.Initialize();
            Container.BindInstance(assetProvider).AsSingle();
        }
    }
}