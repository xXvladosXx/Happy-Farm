using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Utils.Input;
using Codebase.Utils.Raycast;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Codebase.Logic
{
    public class EatableSpawner : MonoBehaviour, IClickable
    {
        private IGameFactory _gameFactory;
        private IRaycastUser _raycastUser;
        private IResourcesStorage _resourcesStorage;

        [Inject]
        public void Construct(IGameFactory gameFactory,
            IRaycastUser raycastUser,
            IResourcesStorage resourcesStorage)
        {
            _gameFactory = gameFactory;
            _raycastUser = raycastUser;
            _resourcesStorage = resourcesStorage;
        }
    
        public void Construct(params IComponent[] components) { }

        public void Interact()
        {
            CreateFood();
        }

        void IClickable.Update() { }

        private async void CreateFood()
        {
            if(!_raycastUser.RaycastHit.HasValue)
                return;
            
            if(_raycastUser.RaycastHit.Value.transform.gameObject.layer != LayerMask.NameToLayer("Ground"))
                return;
            
            await CreateFood(_raycastUser.RaycastHit.Value.point);
        }

        [Button]
        public async UniTask CreateFood(Vector3 position)
        {
            if(!_resourcesStorage.HasResource(ResourceType.Food, 1))
                return;
                
            await _gameFactory.CreateFood("Food", position);
            _resourcesStorage.Remove(ResourceType.Food, 1);
        }
    }
}