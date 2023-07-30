using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codebase.Infrastructure.AssetService;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.Building.Settings;
using Codebase.Logic.Entity.Building.Settings.SpawnPlace;
using Codebase.Logic.Entity.EnemyEntities.Settings;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Codebase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService, IInitializable
    {
        private Dictionary<BuildingTypeID,BuildingSettings> _buildings;
        private Dictionary<BuildingTypeID,StorageSettings> _storages;
        private Dictionary<ProductionAnimalTypeID, ProductionAnimalSettings> _animals;
        private Dictionary<EnemyAnimalTypeID, EnemyAnimalSettings> _enemyAnimals;
        private Dictionary<BuildingTypeID,SpawnPlaceBuildingSettings> _spawnPlaces;

        private Dictionary<BuildingTypeID,FoodProductionSettings> _foodProductions;

        private Dictionary<string, ProductSettings> _products;

        private Dictionary<string, LevelStaticData> _levels;

        public void Initialize()
        {
            LoadBuildings();
            LoadStorages();
            LoadFoodProductions();
            LoadProductionAnimals();
            LoadEnemyAnimals();
            LoadLevels();
            LoadProducts();
            LoadSpawnPlaces();
        }

        public void LoadBuildings()
        {
            _buildings = Resources.LoadAll<BuildingSettings>(AssetPath.BUILDING_SETTINGS)
                .ToDictionary(x => x.BuildingTypeID, x => x);
        }

        public void LoadStorages()
        {
            _storages = Resources.LoadAll<StorageSettings>(AssetPath.STORAGE_SETTINGS)
                .ToDictionary(x => x.BuildingTypeID, x => x);
        }

        public void LoadFoodProductions()
        {
            _foodProductions = Resources.LoadAll<FoodProductionSettings>(AssetPath.FOOD_PRODUCTION_SETTINGS)
                .ToDictionary(x => x.BuildingTypeID, x => x);
        }

        public void LoadProductionAnimals()
        {
            _animals = Resources.LoadAll<ProductionAnimalSettings>(AssetPath.ANIMAL_SETTINGS)
                .ToDictionary(x => x.ProductionAnimalTypeID, x => x);
        }

        public void LoadEnemyAnimals()
        {
            _enemyAnimals = Resources.LoadAll<EnemyAnimalSettings>(AssetPath.ANIMAL_SETTINGS)
                .ToDictionary(x => x.EnemyAnimalTypeID, x => x);
        }

        public void LoadProducts()
        {
            _products = Resources.LoadAll<ProductSettings>(AssetPath.PRODUCT_SETTINGS)
                .ToDictionary(x => x.ID, x => x);
        }

        public void LoadSpawnPlaces()
        {
            _spawnPlaces = Resources.LoadAll<SpawnPlaceBuildingSettings>(AssetPath.SPAWN_PLACE_SETTINGS)
                .ToDictionary(x => x.BuildingTypeID, x => x);
        }

        public void LoadLevels() =>
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.LEVEL_SETTINGS)
                .ToDictionary(x => x.LevelKey, x => x);

        public BuildingSettings GetBuilding(BuildingTypeID buildingTypeId) =>
            _buildings.TryGetValue(buildingTypeId, out var buildingSettings)
                ? buildingSettings : null;

        public StorageSettings GetStorage(BuildingTypeID buildingTypeId) =>
            _storages.TryGetValue(buildingTypeId, out var storageSettings)
                ? storageSettings : null;

        public FoodProductionSettings GetFoodProduction(BuildingTypeID buildingTypeId) =>
            _foodProductions.TryGetValue(buildingTypeId, out var foodProductionSettings)
                ? foodProductionSettings : null;

        public SpawnPlaceBuildingSettings GetSpawnPlace(BuildingTypeID buildingTypeId) =>
            _spawnPlaces.TryGetValue(buildingTypeId, out var placeBuildingSettings)
                ? placeBuildingSettings : null;

        public ProductionAnimalSettings GetProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID) =>
            _animals.TryGetValue(productionAnimalTypeID, out var animalSettings)
                ? animalSettings : null;

        public EnemyAnimalSettings GetEnemyAnimal(EnemyAnimalTypeID enemyAnimalTypeID) =>
            _enemyAnimals.TryGetValue(enemyAnimalTypeID, out var animalSettings)
                ? animalSettings : null;

        public ProductSettings GetProduct(string productTypeId) =>
            _products.TryGetValue(productTypeId, out var productSettings)
                ? productSettings : null;

        public LevelStaticData ForLevel(string sceneKey)
        {
            return  _levels.TryGetValue(sceneKey, out var playerSettings) 
                ? playerSettings : null;
        }
    }
}