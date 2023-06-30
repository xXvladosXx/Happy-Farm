using Codebase.Logic.QuestSystem;
using Codebase.Logic.QuestSystem.Core;
using Codebase.Utils.Raycast;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private RaycastSettings _raycastSettings;
        [SerializeField] private MissionCatalog _missionCatalog;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ClickUser>().AsSingle();
            Container.Bind<RaycastSettings>().FromScriptableObject(_raycastSettings).AsSingle();
            Container.Bind<MissionCatalog>().FromScriptableObject(_missionCatalog).AsSingle();
            Container.Bind<IRaycastUser>().To<RaycastUser>().AsSingle();
            
            CreateMissionsManager();
        }

        private void CreateMissionsManager()
        {
            Container.Bind<MissionRequires>().AsSingle();
            Container.BindInterfacesAndSelfTo<MissionsManager>().AsSingle();
        }
    }
}