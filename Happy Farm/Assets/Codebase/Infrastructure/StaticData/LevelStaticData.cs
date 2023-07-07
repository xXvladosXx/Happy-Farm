using System;
using System.Collections.Generic;
using Codebase.Logic.Entity.Building;
using Codebase.Logic.QuestSystem.Core;
using UnityEngine;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<BuildingSpawnerData> BuildingSpawners = new List<BuildingSpawnerData>();
        public MissionCatalog MissionCatalog;
    }

    [Serializable]
    public class BuildingSpawnerData
    {
        public BuildingTypeID BuildingTypeID;
        public Vector3 Position;

        public BuildingSpawnerData(BuildingTypeID buildingTypeId, Vector3 position)
        {
            BuildingTypeID = buildingTypeId;
            Position = position;
        }
    }
}