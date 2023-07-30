using System;
using System.Collections.Generic;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.Entity.ProductionEntities.Production.Resource;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Codebase.Logic.QuestSystem.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<BuildingSpawnerData> BuildingSpawners = new();
        public List<EnemySpawnerData> EnemySpawners = new();
        public List<ResourcesData> Resources = new();
        public MissionCatalog MissionCatalog;
    }

    [Serializable]
    public class BuildingSpawnerData
    {
        public BuildingTypeID BuildingTypeID;
        public Vector3 Position;

        public BuildingSpawnerData(BuildingTypeID buildingTypeId,
            Vector3 position)
        {
            BuildingTypeID = buildingTypeId;
            Position = position;
        }
    }

    [Serializable]
    public class PortalParticleData
    {
        public ParticleSystem SpawnEffectStart;
        public ParticleSystem SpawnEffectIdle;
        public ParticleSystem SpawnEffectClose;
    }
    
    [Serializable]
    public class EnemySpawnerData
    {
        public PortalParticleData PortalParticleData;
        public bool IsLooped;
        public EnemyAnimalTypeID EnemyTypeID;
        public Vector3 Position;
        public float TimeToSpawn;
        
        public EnemySpawnerData(EnemyAnimalTypeID enemyTypeId,
            Vector3 position,
            float timeToSpawn,
            bool isLooped, 
            PortalParticleData portalParticleData)
        {
            PortalParticleData = portalParticleData;
            IsLooped = isLooped;
            EnemyTypeID = enemyTypeId;
            Position = position;
            TimeToSpawn = timeToSpawn;
        }
    }

    [Serializable]
    public class ResourcesData
    {
        public ResourceType Type;
        public int Amount;
    }
}