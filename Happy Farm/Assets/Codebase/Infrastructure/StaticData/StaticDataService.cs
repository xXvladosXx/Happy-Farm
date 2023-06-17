using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codebase.Infrastructure.AssetService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<BuildingTypeID,BuildingSettings> _buildings;
        private Dictionary<AnimalTypeID, AnimalSettings> _animals;
        private Dictionary<string, ProductSettings> _products;
        private Dictionary<string, LevelStaticData> _levels;
        
        private readonly IAssetProvider _assetProvider;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public void LoadBuildings()
        {
            _buildings = Resources.LoadAll<BuildingSettings>(AssetPath.BUILDING_SETTINGS)
                .ToDictionary(x => x.BuildingTypeID, x => x);
        }

        public void LoadAnimals()
        {
            _animals = Resources.LoadAll<AnimalSettings>(AssetPath.ANIMAL_SETTINGS)
                .ToDictionary(x => x.AnimalTypeID, x => x);
        }

        public void LoadProducts()
        {
            _products = Resources.LoadAll<ProductSettings>(AssetPath.PRODUCT_SETTINGS)
                .ToDictionary(x => x.ID, x => x);
        }

        public void LoadLevels()
        {
        }

        public BuildingSettings GetBuilding(BuildingTypeID buildingTypeId) =>
            _buildings.TryGetValue(buildingTypeId, out var buildingSettings)
                ? buildingSettings : null;

        public AnimalSettings GetAnimal(AnimalTypeID animalTypeId) =>
            _animals.TryGetValue(animalTypeId, out var animalSettings)
                ? animalSettings : null;

        public ProductSettings GetProduct(string productTypeId) =>
            _products.TryGetValue(productTypeId, out var productSettings)
                ? productSettings : null;

        public LevelStaticData ForLevel(string sceneKey)
        {
            return null;
        }
    }
}