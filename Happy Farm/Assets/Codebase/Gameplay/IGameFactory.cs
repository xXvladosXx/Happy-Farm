﻿using System.Threading.Tasks;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.EnemyEntities;
using Codebase.Logic.Entity.ProductionEntities;
using Codebase.Logic.Entity.ProductionEntities.Eating;
using Codebase.Logic.QuestSystem.Core;
using Codebase.Logic.Storage;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Gameplay
{
    public interface IGameFactory
    {
        UniTask CreatePlayer();
        UniTask<ProductionAnimal> CreateProductionAnimal(ProductionAnimalTypeID productionAnimalTypeID);
        UniTask<EnemyAnimal> CreateEnemyAnimal(EnemyAnimalTypeID productionAnimalTypeID);
        UniTask<Eatable> CreateFood(string foodName, Vector3 position);
        UniTask<Storage> CreateStorage(BuildingTypeID buildingTypeID, Vector3 position);
        UniTask<GameObject> CreateProduct(string productId, Vector3 position, int productionAmount);
        UniTask<ProductionConstruction> CreateProductionConstruction(BuildingTypeID buildingTypeID, Vector3 position);
        UniTask CreateProductionSpawner(BuildingTypeID buildingTypeID, Vector3 buildingSpawnerPosition);
        UniTask CreateUI();
    }
}