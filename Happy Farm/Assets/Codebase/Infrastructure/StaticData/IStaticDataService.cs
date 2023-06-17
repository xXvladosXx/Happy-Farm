using System.Threading.Tasks;

namespace Codebase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void LoadBuildings();
        void LoadAnimals();
        void LoadLevels();
        void LoadProducts();
        BuildingSettings GetBuilding(BuildingTypeID buildingTypeId);
        AnimalSettings GetAnimal(AnimalTypeID animalTypeId);
        ProductSettings GetProduct(string productTypeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}