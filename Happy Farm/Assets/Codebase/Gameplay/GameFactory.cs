using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Infrastructure.AssetService;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Animations;
using Codebase.Logic.Animations.AnimationsReader;
using Codebase.Logic.Entity;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.EnemyEntities.Catch;
using Codebase.Logic.Entity.Movement;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.ProductionEntities.Production;
using Codebase.Logic.Entity.Stats;
using Codebase.Logic.Storage;
using Codebase.Logic.Storage.Container;
using Codebase.Utils.Raycast;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Codebase.Gameplay
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly EatableRegistry _eatableRegistry;
        private IContainer _inventory;

        public GameFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            EatableRegistry eatableRegistry)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _eatableRegistry = eatableRegistry;
        }

        public async UniTask CreatePlayer() => 
            await _assetProvider.Load<GameObject>(AssetPath.PLAYER);

        public async UniTask<ProductionAnimal> CreateProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID)
        {
            var setting = _staticDataService.GetProductionAnimal(productionAnimalTypeID);
            var animal = await _assetProvider.Load<GameObject>(setting.AnimalPrefab);
            var animalInstance = Object.Instantiate(animal, Vector3.zero, Quaternion.identity);
            var health = new Health(setting.Health);
            var producer = new InstantProducer(this, setting.ProductionName,1);
            var eater = new AnimalEater(setting.HungerThreshold, setting.EatingRate, setting.EatingAmount, 
                setting.HungerRate, health, producer, animalInstance.transform, setting.HungerAmount);
            var movement = new NavMeshMovement(animalInstance.GetComponent<NavMeshAgent>(), setting.IdleSpeed, setting.RunSpeed);

            var healthDestroyable = animalInstance.GetComponent<HealthDestroyable>();
            healthDestroyable.Construct(health);
            healthDestroyable.Initialize();
            
            var animatorStateReader = animalInstance.GetComponent<AnimalStateReader>();
            var animatorStateHasher = await _assetProvider.Load<AnimatorStateHasher>(setting.AnimatorStateHasher);
            animatorStateHasher.Init();
            animatorStateReader.Construct(animatorStateHasher, animalInstance.GetComponent<Animator>(), movement, .2f);
            
            var productionAnimal = new ProductionAnimal(eater, movement, animalInstance.transform,
                _eatableRegistry, producer, animatorStateReader);
            productionAnimal.BindTransitions();
            productionAnimal.Initialize();
            
            return productionAnimal;
        }

        public async UniTask<EnemyAnimal> CreateEnemyAnimal(EnemyAnimalTypeID productionAnimalTypeID)
        {
            var setting = _staticDataService.GetEnemyAnimal(productionAnimalTypeID);
            var animal = await _assetProvider.Load<GameObject>(setting.AnimalPrefab);
            var animalInstance = Object.Instantiate(animal, Vector3.zero, Quaternion.identity);
            var catchable = new Catchable(setting.ClickAmountToCatch,setting.TimeToCatch,setting.MaxTimeToWaitCaught);
            var movement = new NavMeshMovement(animalInstance.GetComponent<NavMeshAgent>(), setting.IdleSpeed, setting.RunSpeed);
            var collectable = new Collectable(_inventory, setting.Item, 1);
            
            var animatorStateReader = animalInstance.GetComponent<AnimalStateReader>();
            
            var animatorStateHasher = await _assetProvider.Load<AnimatorStateHasher>(setting.AnimatorStateHasher);
            animatorStateHasher.Init();
            animatorStateReader.Construct(animatorStateHasher, animalInstance.GetComponent<Animator>(), movement, .2f);

            var animationPlayer = new AnimationPlayer(animatorStateReader);

            var clickable = animalInstance.GetComponent<Clickable>();
            clickable.Construct(collectable,catchable,animationPlayer);
            var enemyAnimal = new EnemyAnimal(animalInstance.transform, movement, catchable, collectable, animatorStateReader);
            enemyAnimal.BindTransitions();
            
            return enemyAnimal;
        }

        public async UniTask<Eatable> CreateFood(string foodName, Vector3 position)
        {
            var go = await _assetProvider.Load<GameObject>(foodName);
            var eatable = go.GetComponent<Eatable>();
            var health = new Health(10);
            var foodInstance = Object.Instantiate(eatable, position, Quaternion.identity);
            foodInstance.Construct(health);
            
            return foodInstance;
        }

        public async UniTask<Storage> CreateStorage(BuildingTypeID buildingTypeID, Vector3 position)
        {
            var setting = _staticDataService.GetStorage(buildingTypeID);

            var go = await _assetProvider.Load<GameObject>(setting.BuildingPrefab);
            var storageInstance = Object.Instantiate(go, position, Quaternion.identity);
            var items = new List<ISlot>();
            
            _inventory ??= new ItemContainer(setting.Capacity);
            
            foreach (var slot in _inventory.Slots)
            {
                items.Add(!slot.IsEmpty ? new Slot(slot.Item, slot.CurrentAmount) : new Slot());
            }
            
            _inventory = new ItemContainer(items, setting.Capacity);

            return storageInstance.GetComponent<Storage>();
        }

        public async UniTask<GameObject> CreateProduct(string productId, Vector3 position, int productionAmount)
        {
            var setting = _staticDataService.GetProduct(productId);

            var go = await _assetProvider.Load<GameObject>(setting.ProductPrefab);
            var productInstance = Object.Instantiate(go, position, Quaternion.identity);
            
            var collectable = new Collectable(_inventory, setting.Item, productionAmount)
            {
                CanBeCollected = true
            };

            var clickable = productInstance.AddComponent<Clickable>();
            clickable.Construct(collectable);
            
            Debug.Log($"Product created {collectable}");
            
            return productInstance;
        }
        
        public async UniTask<ProductionConstruction> CreateProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position)
        {
            var setting = _staticDataService.GetBuilding(buildingTypeID);

            var go = await _assetProvider.Load<GameObject>(setting.BuildingPrefab);
            var construction = Object.Instantiate(go, position, Quaternion.identity);
            var producer = new TimeProducer(this, setting.Item, setting.ProductionTime);
            var consumer = new ItemConsumer(setting.ConsumptionItem, setting.ConsumptionAmount);
            var factory = new Factory(producer, consumer, _inventory);
            
            var clickable = construction.AddComponent<Clickable>();
            clickable.Construct(factory);
            
            var productionConstruction = new ProductionConstruction(producer, consumer, construction.transform);
            productionConstruction.BindTransitions();
            
            return productionConstruction;
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
                    new MoneyRequirement(upgrade.Cost, 4444)
                });
            }

            IBuildable buildable = setting.BuildingType switch
            {
                ProductionConstructionBuildable => new ProductionConstructionBuildable(this),
                StorageConstructionBuildable => new StorageConstructionBuildable(this),
                _ => throw new ArgumentOutOfRangeException()
            };

            var spawnable = new Spawnable(upgrades, buildable);
            
            var clickable = productInstance.GetComponent<Clickable>();
            clickable.Construct(spawnable);
        }

        public void RegisterSavable(GameObject entity)
        {

        }

        public Task CreateUI()
        {
            throw new System.NotImplementedException();
        }
    }
}