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
    public class AnimalSpawner : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private List<ProductionAnimal> _productionAnimals = new List<ProductionAnimal>();
        private List<EnemyAnimal> _enemyAnimals = new List<EnemyAnimal>();
        private List<ProductionConstruction> _constructions = new List<ProductionConstruction>();
        private AnimalRegistry _animalRegistry;

        [Inject]
        public void Construct(IGameFactory gameFactory,
            AnimalRegistry animalRegistry)
        {
            _gameFactory = gameFactory;
            _animalRegistry = animalRegistry;
        }

        [Button]
        public async UniTask CreateAnimal()
        {
            await _gameFactory.CreateProductionAnimal(ProductionAnimalTypeID.Cow);
        }

        [Button]
        private async UniTask CreateEnemyAnimal()
        {
            var animal = await _gameFactory.CreateEnemyAnimal(EnemyAnimalTypeID.Bear);
            _enemyAnimals.Add(animal);
        }

        private async UniTask CreateConstruction()
        {
            var construction = await _gameFactory.CreateProductionConstruction(BuildingTypeID.WoolFactoryFirst, Vector3.zero);
            _constructions.Add(construction);
        }

        [Button]
        private async UniTask CreateSpawnerConstruction()
        {
             await _gameFactory.CreateProductionSpawner(BuildingTypeID.WoolFactoryFirst, Vector3.zero);
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) CreateAnimal();
            if(Input.GetKeyDown(KeyCode.T)) CreateEnemyAnimal();
            if (Input.GetKeyDown(KeyCode.L)) CreateConstruction();


            for (int i = 0; i < _enemyAnimals.Count; i++)
            {
                _enemyAnimals[i].Update();
            }
        }
    }
}