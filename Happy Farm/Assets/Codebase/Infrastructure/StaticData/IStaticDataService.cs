using System.Threading.Tasks;

namespace Codebase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void LoadBuildings();
        void LoadStorages();
        void LoadProductionAnimals();
        void LoadEnemyAnimals();
        void LoadLevels();
        void LoadProducts();
        void LoadSpawnPlaces();
        
        BuildingSettings GetBuilding(BuildingTypeID buildingTypeId);
        StorageSettings GetStorage(BuildingTypeID buildingTypeId);
        SpawnPlaceBuildingSettings GetSpawnPlace(BuildingTypeID buildingTypeId);
        ProductionAnimalSettings GetProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID);
        EnemyAnimalSettings GetEnemyAnimal(EnemyAnimalTypeID enemyAnimalTypeID);
        ProductSettings GetProduct(string productTypeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}