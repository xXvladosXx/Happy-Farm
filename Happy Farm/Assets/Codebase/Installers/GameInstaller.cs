﻿using Codebase.Gameplay;
using Codebase.Infrastructure;
using Codebase.Infrastructure.AssetService;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StateMachine;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.ProductionEntities.Eating;
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
            Container.Bind<EatableRegistry>().AsSingle();
            Container.Bind<GameFactory>().AsSingle();
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService(Container.Resolve<IAssetProvider>());
            staticDataService.LoadBuildings();
            staticDataService.LoadAnimals();
            staticDataService.LoadLevels();
            staticDataService.LoadProducts();
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