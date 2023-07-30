using System;
using System.Collections.Generic;
using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Codebase.UI.AnimalSpawner
{
    public class SpawnerUI : SerializedMonoBehaviour 
    {
        [SerializeField] private Dictionary<ProductionAnimalTypeID, Button> _animals;
        
        public void Initialize(Logic.Entity.ProductionEntities.AnimalSpawner animalSpawner)
        {
            foreach (var animal in _animals)
            {
                animal.Value.onClick.AddListener(() => SpawnAnimal(animal.Key, animalSpawner));
            }
        }

        public void Dispose()
        {
            foreach (var animal in _animals)
            {
                animal.Value.onClick.RemoveAllListeners();
            }
        }

        private void SpawnAnimal(ProductionAnimalTypeID productionAnimalTypeID, Logic.Entity.ProductionEntities.AnimalSpawner animalSpawner)
        {
            animalSpawner.CreateProductionAnimal(productionAnimalTypeID);
        }
    }
}