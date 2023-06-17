using System.Threading.Tasks;
using Codebase.Infrastructure.AssetService;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Animations;
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
        
        public void CreatePlayerStorage()
        {
            _inventory = new ItemContainer(10);
        }

        public async UniTask<ProductionAnimal> CreateProductionAnimal(AnimalTypeID animalTypeID)
        {
            var setting = _staticDataService.GetAnimal(animalTypeID);
            var animal = await _assetProvider.Load<GameObject>(setting.AnimalPrefab);
            var animalInstance = Object.Instantiate(animal, Vector3.zero, Quaternion.identity);
            var health = new Health(100);
            var eater = new Eater(20, 2, 4, health);
            var movement = new NavMeshMovement(animalInstance.GetComponent<NavMeshAgent>(), 10);
            var producer = new Producer(this, "Milk");
            
            var healthDestroyable = animalInstance.GetComponent<HealthDestroyable>();
            healthDestroyable.Construct(health);
            healthDestroyable.Initialize();
            
            var productionAnimal = new ProductionAnimal(eater, movement, animalInstance.transform, _eatableRegistry, producer);
            productionAnimal.BindTransitions();
            productionAnimal.Initialize();
            
            return productionAnimal;
        }

        public async UniTask<EnemyAnimal> CreateEnemyAnimal(AnimalTypeID animalTypeID)
        {
            var setting = _staticDataService.GetAnimal(animalTypeID);
            var animal = await _assetProvider.Load<GameObject>(setting.AnimalPrefab);
            var animalInstance = Object.Instantiate(animal, Vector3.zero, Quaternion.identity);
            var catchable = new Catchable(5,2,5);
            var movement = new NavMeshMovement(animalInstance.GetComponent<NavMeshAgent>(), 10);
            var collectable = new Collectable(_inventory, setting.Item);
            
            var animatorStateReader = animalInstance.GetComponent<AnimalStateReader>();
            var animatorStateHasher = _assetProvider.GetAsset<AnimatorStateHasher>("Enemy State Hasher");
            animatorStateHasher.Init();
            animatorStateReader.Construct(animatorStateHasher, animalInstance.GetComponent<Animator>(),
                animalInstance.GetComponent<Rigidbody>(), 4);

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

        public async UniTask<GameObject> CreateProduct(string productId, Vector3 position)
        {
            var setting = _staticDataService.GetProduct(productId);

            var go = await _assetProvider.Load<GameObject>(setting.ProductPrefab);
            var productInstance = Object.Instantiate(go, position, Quaternion.identity);
            
            var collectable = new Collectable(_inventory, setting.Item);
            collectable.CanBeCollected = true;
            
            var clickable = productInstance.AddComponent<Clickable>();
            clickable.Construct(collectable);

            return productInstance;
        }
        
        public async UniTask<ProductionConstruction> CreateProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position)
        {
            var setting = _staticDataService.GetBuilding(buildingTypeID);

            var go = await _assetProvider.Load<GameObject>(setting.BuildingPrefab);
            var construction = Object.Instantiate(go, position, Quaternion.identity);
            var producer = new Producer(this, "Whool");
            var consumer = new ItemConsumer(_inventory, "Milk", 1);
            
            var clickable = construction.AddComponent<Clickable>();
            clickable.Construct(consumer);
            
            var productionConstruction = new ProductionConstruction(producer, consumer, construction.transform);
            productionConstruction.Initialize();
            productionConstruction.BindTransitions();
            
            return productionConstruction;
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