using Codebase.Logic.Entity.EnemyEntities.Catch;
using Codebase.Logic.Storage;
using Codebase.Utils.Raycast;
using UnityEngine;
using Zenject;

namespace Codebase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private RaycastSettings _raycastSettings;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ClickUser>().AsSingle();
            Container.Bind<RaycastSettings>().FromScriptableObject(_raycastSettings).AsSingle();
            Container.Bind<IRaycastUser>().To<RaycastUser>().AsSingle();
        }
    }
}