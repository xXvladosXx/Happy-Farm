using System.Threading.Tasks;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.Building.Settings;
using Codebase.Logic.Entity.Building.Settings.SpawnPlace;
using Codebase.Logic.Entity.EnemyEntities.Settings;
using Codebase.Logic.Entity.ProductionEntities.Settings;

namespace Codebase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void LoadBuildings();
        void LoadStorages();
        void LoadFoodProductions();
        void LoadProductionAnimals();
        void LoadEnemyAnimals();
        void LoadLevels();
        void LoadProducts();
        void LoadSpawnPlaces();
        
        BuildingSettings GetBuilding(BuildingTypeID buildingTypeId);
        StorageSettings GetStorage(BuildingTypeID buildingTypeId);
        FoodProductionSettings GetFoodProduction(BuildingTypeID buildingTypeId);
        SpawnPlaceBuildingSettings GetSpawnPlace(BuildingTypeID buildingTypeId);
        ProductionAnimalSettings GetProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID);
        EnemyAnimalSettings GetEnemyAnimal(EnemyAnimalTypeID enemyAnimalTypeID);
        ProductSettings GetProduct(string productTypeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}