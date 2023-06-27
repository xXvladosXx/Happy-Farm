using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebase.Gameplay;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.Entity.Stats;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Codebase.Logic
{
    public class AnimalSpawner : MonoBehaviour
    {
        private GameFactory _gameFactory;
        private List<ProductionAnimal> _productionAnimals = new List<ProductionAnimal>();
        private List<EnemyAnimal> _enemyAnimals = new List<EnemyAnimal>();
        private List<ProductionConstruction> _constructions = new List<ProductionConstruction>();

        [Inject]
        public void Construct(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        [Button]
        public async UniTask CreateAnimal()
        {
            var animal = await _gameFactory.CreateProductionAnimal(ProductionAnimalTypeID.Cow);
            _productionAnimals.Add(animal);
            var destroyable = animal.Transform.GetComponent<IDestroyable>();
            destroyable.OnDestroyed += OnAnimalDied;
            
            void OnAnimalDied()
            {
                destroyable.OnDestroyed -= OnAnimalDied;
                animal.Dispose();
                _productionAnimals.Remove(animal);
            }
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

            for (int i = 0; i < _constructions.Count; i++)
            {
                _constructions[i].Update();
            }
            
            for (int i = 0; i < _productionAnimals.Count; i++)
            {
                _productionAnimals[i].Update();
            }

            for (int i = 0; i < _enemyAnimals.Count; i++)
            {
                _enemyAnimals[i].Update();
            }
        }
    }
}