﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Infrastructure.AssetService;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Animations;
using Codebase.Logic.Animations.AnimationsReader;
using Codebase.Logic.Entity;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.Building.Constructions;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.EnemyEntities.Attack;
using Codebase.Logic.Entity.EnemyEntities.Catch;
using Codebase.Logic.Entity.Movement;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.ProductionEntities.Production.Consumers;
using Codebase.Logic.Entity.ProductionEntities.Production.Producers;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Codebase.Logic.Stats;
using Codebase.Logic.Storage;
using Codebase.Logic.Storage.Container;
using Codebase.Logic.Upgrades;
using Codebase.UI;
using Codebase.UI.Inventory;
using Codebase.Utils.Cheat;
using Codebase.Utils.Raycast;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Codebase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IResourcesStorage _resourcesStorage;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStorageUser _storageUser;
        private readonly GameBehaviourHandler _gameBehaviour;

        public GameFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IStorageUser storageUser,
            IResourcesStorage resourcesStorage,
            ICoroutineRunner coroutineRunner,
            GameBehaviourHandler gameBehaviourHandler)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _storageUser = storageUser;
            _gameBehaviour = gameBehaviourHandler;
            _resourcesStorage = resourcesStorage;
            _coroutineRunner = coroutineRunner;
        }

        public async UniTask CreatePlayer()
        {
            await _assetProvider.Load<GameObject>(AssetPath.PLAYER);
        }

        public async UniTask<ProductionAnimal> CreateProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID)
        {
            var setting = _staticDataService.GetProductionAnimal(productionAnimalTypeID);
            var animal = await _assetProvider.Load<GameObject>(setting.AnimalPrefab);
            var animalInstance = Object.Instantiate(animal, Vector3.zero, Quaternion.identity);
            var health = new Health(setting.Health);
            var producer = new InstantItemProducer(this, setting.ProductionName,1, animalInstance.transform);
            var eater = new Eater(setting.HungerThreshold, setting.EatingRate, setting.EatingAmount, 
                setting.HungerRate, health, producer, animalInstance.transform, setting.HungerAmount);
            var movement = new NavMeshMovement(animalInstance.GetComponent<NavMeshAgent>(), setting.IdleSpeed, setting.RunSpeed);
            
            var animatorStateReader = animalInstance.GetComponent<AnimalStateReader>();
            var animatorStateHasher = await _assetProvider.Load<AnimatorStateHasher>(setting.AnimatorStateHasher);
            animatorStateHasher.Init();
            animatorStateReader.Construct(animatorStateHasher, animalInstance.GetComponent<Animator>(), movement, .2f);

            var destroyable = new Destoyable(animalInstance);

            var productionAnimal = new ProductionAnimal(eater, movement, 
                animalInstance.transform, _gameBehaviour, 
                animatorStateReader, destroyable);
            
            productionAnimal.BindTransitions();
            productionAnimal.Initialize();
            
            _gameBehaviour.Add(productionAnimal);
            
            return productionAnimal;
        }

        public async UniTask<EnemyAnimal> CreateEnemyAnimal(EnemyAnimalTypeID productionAnimalTypeID, Vector3 position)
        {
            var setting = _staticDataService.GetEnemyAnimal(productionAnimalTypeID);
            var animal = await _assetProvider.Load<GameObject>(setting.AnimalPrefab);
            var animalInstance = Object.Instantiate(animal, position, Quaternion.identity);
            var catchable = new Catchable(setting.ClickAmountToCatch,setting.TimeToCatch,setting.MaxTimeToWaitCaught);
            var movement = new NavMeshMovement(animalInstance.GetComponent<NavMeshAgent>(), setting.IdleSpeed, setting.RunSpeed);
            var destroyable = new Destoyable(animalInstance);
            var collectable = new Collectable(_storageUser, setting.Item, 1, destroyable);
            
            var animatorStateReader = animalInstance.GetComponent<AnimalStateReader>();
            
            var animatorStateHasher = await _assetProvider.Load<AnimatorStateHasher>(setting.AnimatorStateHasher);
            animatorStateHasher.Init();
            animatorStateReader.Construct(animatorStateHasher, animalInstance.GetComponent<Animator>(), movement, .2f);

            var animationPlayer = new AnimationPlayer(animatorStateReader);
            
            var clickable = animalInstance.GetComponent<Clickable>();
            clickable.Construct(collectable,catchable,animationPlayer);
            var enemyAnimal = new EnemyAnimal(animalInstance.transform, movement, catchable, collectable, animatorStateReader);
            enemyAnimal.BindTransitions();
            
            _gameBehaviour.Add(enemyAnimal);
            _gameBehaviour.Add(collectable);
            
            return enemyAnimal;
        }

        public async UniTask<Eatable> CreateFood(EatableTypeID eatableTypeID, Vector3 position)
        {
            var eatableSettings = _staticDataService.GetEatable(eatableTypeID); 
            var go = await _assetProvider.Load<GameObject>(eatableSettings.Prefab);
            var gameObject = Object.Instantiate(go, position, Quaternion.identity);
            var health = new Health(eatableSettings.EatableHealthData.Health);
            var destroyable = new Destoyable(gameObject);
            var eatable = new Eatable(health, gameObject.transform, destroyable, eatableSettings.EatableHealthData);
            
            _gameBehaviour.Add(eatable);
            
            return eatable;
        }

        public async UniTask<Construction> CreateStorage(BuildingTypeID buildingTypeID, Vector3 position)
        {
            var setting = _staticDataService.GetStorage(buildingTypeID);

            var go = await _assetProvider.Load<GameObject>(setting.BuildingPrefab);
            var storageInstance = Object.Instantiate(go, position, Quaternion.identity);
            
            var cheater = Object.FindObjectOfType<Cheater>();
            cheater.Construct(_storageUser);
            _storageUser.Inventory.IncreaseCapacity(setting.Capacity);

            var destroyable = new Destoyable(storageInstance);
            var construction = new Construction(buildingTypeID, destroyable, storageInstance.transform);
                
            return construction;
        }

        public async UniTask<GameObject> CreateProduct(string productId, Vector3 position, int productionAmount)
        {
            var setting = _staticDataService.GetProduct(productId);

            var go = await _assetProvider.Load<GameObject>(setting.ProductPrefab);
            var productInstance = Object.Instantiate(go, position, Quaternion.identity);
            var destroyable = new Destoyable(productInstance);
            var collectable = new Collectable(_storageUser, setting.Item, productionAmount, destroyable)
            {
                CanBeCollected = true
            };

            var clickable = productInstance.AddComponent<Clickable>();
            clickable.Construct(collectable);
            
            Debug.Log($"Product created {collectable}");
            
            _gameBehaviour.Add(collectable);
            
            return productInstance;
        }

        public async UniTask<ProductionConstruction> CreateResourceProductionConstruction(BuildingTypeID buildingTypeID,
            Vector3 position, ResourceType productionResourceType, ResourceType consumptionResourceType)
        {
            var setting = _staticDataService.GetFoodProduction(buildingTypeID);
            
            _resourcesStorage.IncreaseMaxAmount(productionResourceType, setting.ProductionAmount);
            
            var go = await _assetProvider.Load<GameObject>(setting.BuildingPrefab);
            var construction = Object.Instantiate(go, position, Quaternion.identity);
            var producer = new TimeableResourceProducer(this, setting.ProductionAmount, setting.ProductionTime,
                construction.transform, productionResourceType, _resourcesStorage);
            IConsumer<ResourceType, int> consumer = new ResourceConsumer(consumptionResourceType, _resourcesStorage, setting.Cost);
            var factory = new ProductFactory<ResourceType>(producer, consumer);

            var clickable = construction.AddComponent<Clickable>();
            clickable.Construct(factory);
            
            var upgradeDestroyable = new Destoyable(construction);
            var productionConstruction = new ProductionConstruction(producer, construction.transform, upgradeDestroyable, buildingTypeID);
            productionConstruction.BindTransitions();
            
            _gameBehaviour.Add(productionConstruction);

            return productionConstruction;
        }
        
        public async UniTask<IDestroyable> CreateProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position)
        {
            var setting = _staticDataService.GetBuilding(buildingTypeID);
            var go = await _assetProvider.Load<GameObject>(setting.BuildingPrefab);
            var construction = Object.Instantiate(go, position, Quaternion.identity);
            var producer = new TimeableItemProducer(this, setting.Item, setting.ProductionTime, 
                construction.GetComponent<ProductionPoint>().SpawnPoint, setting.ProductionAmount);
            IConsumer<string, int> consumer = new ItemConsumer(_storageUser, setting.ConsumptionItem, setting.ConsumptionAmount);
            var factory = new ProductFactory<string>(producer, consumer);
            
            var clickable = construction.AddComponent<Clickable>();
            clickable.Construct(factory);
            
            var destroyable = new Destoyable(construction);
            var productionConstruction = new ProductionConstruction(producer, construction.transform, destroyable, buildingTypeID);
            productionConstruction.BindTransitions();
            
            _gameBehaviour.Add(productionConstruction);
            
            return destroyable;
        }
        
        
        public async UniTask CreateProductionSpawner(BuildingTypeID buildingTypeID, Vector3 buildingSpawnerPosition)
        {
            var setting = _staticDataService.GetSpawnPlace(buildingTypeID);

            var go = await _assetProvider.Load<GameObject>(setting.SpawnPlacePrefab);
            var productInstance = Object.Instantiate(go, buildingSpawnerPosition, Quaternion.identity);
            var upgrades = new Dictionary<Upgrade, List<IRequirement>>();
            foreach (var upgrade in setting.Upgrades)
            {
                upgrades.Add(upgrade, new List<IRequirement>
                {
                    new MoneyRequirement(upgrade.Cost, _resourcesStorage)
                });
            }

            var buildable = setting.CreateBuilding(this);

            var spawnable = new BuildingUpgrader(upgrades, buildable);
            if (setting.BuildingTypeID != BuildingTypeID.None)
            {
                await spawnable.Upgrade(productInstance.GetComponent<BuildingSpawnPoint>().SpawnPoint);   
            }
            
            var clickable = productInstance.GetComponent<Clickable>();
            clickable.Construct(spawnable);
        }

        public void CreateEnemySpawner(EnemyAnimalTypeID enemyAnimalTypeID, Vector3 spawnPosition, float time,
            bool isLooped, PortalParticleData enemySpawnerPortalParticleData)
        {
            var enemySpawner = new EnemySpawner(this, enemyAnimalTypeID, spawnPosition, time, isLooped, enemySpawnerPortalParticleData, _coroutineRunner);
            enemySpawner.Initialize();
        }
        
        public async UniTask CreateUI()
        {
            var ui = await _assetProvider.Load<GameObject>(AssetPath.UI_PATH);
        }

        public void CreateResources(List<ResourcesData> levelDataResources)
        {
            foreach (var resourcesData in levelDataResources)
            {
                _resourcesStorage.Add(resourcesData.Type, resourcesData.Amount);
            }
        }
    }
}