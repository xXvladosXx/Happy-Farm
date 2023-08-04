using System.Collections.Generic;
using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Entity
{
    public class AnimalSpawnerTest : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private List<ProductionAnimal> _productionAnimals = new List<ProductionAnimal>();
        private List<ProductionConstruction> _constructions = new List<ProductionConstruction>();


        [Button]
        public async UniTask CreateAnimal()
        {
            await _gameFactory.CreateProductionAnimal(ProductionAnimalTypeID.Cow);
        }


        [Button]
        private async UniTask CreateSpawnerConstruction()
        {
             await _gameFactory.CreateProductionSpawner(BuildingTypeID.WoolFactoryFirst, Vector3.zero);
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) CreateAnimal();
        }
    }
}