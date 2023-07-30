using Codebase.Infrastructure;
using Codebase.Infrastructure.AssetService;
using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.ShopSystem;
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
            Container.BindInterfacesAndSelfTo<EnemyAnimalRegistry>().AsSingle();
        }

        private void CreateInventory()
        {
            Container.Bind<IContainer>().To<ItemContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesStorage>().AsSingle();
            Container.Bind<IStorageUser>().To<StorageUser>().AsSingle();
            Container.BindInterfacesAndSelfTo<Shop>().AsSingle();
        }

        private void RegisterStaticData()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }

        private void RegisterAssetProvider()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        }
    }
}