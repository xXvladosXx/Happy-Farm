using Codebase.Controllers;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.QuestSystem;
using Codebase.Logic.QuestSystem.Core;
using Codebase.UI;
using Codebase.Utils.Raycast;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private RaycastSettings _raycastSettings;
        [SerializeField] private MissionCatalog _missionCatalog;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ClickUser>().AsSingle();
            Container.Bind<AnimalSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalSpawnerController>().AsSingle();
            Container.Bind<RaycastSettings>().FromScriptableObject(_raycastSettings).AsSingle();
            
            Container.Bind<GameplayUI>().FromInstance(_gameplayUI).AsSingle();
            
            Container.BindInterfacesAndSelfTo<InventoryController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<QuestController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<RaycastUser>().AsSingle();
            
            CreateMissionsManager();
        }

        private void CreateMissionsManager()
        {
            Container.Bind<MissionCatalog>().FromScriptableObject(_missionCatalog).AsSingle();
            Container.Bind<MissionRequires>().AsSingle();
            Container.BindInterfacesAndSelfTo<MissionsCollector>().AsSingle();
        }
    }
}