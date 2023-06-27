using System.Collections.Generic;
using Codebase.Logic.Entity.Building;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "SpawnPlaceBuildingSettings", menuName = "StaticData/SpawnPlaceBuildingSettings")]
    public class SpawnPlaceBuildingSettings : SerializedScriptableObject
    {
        public BuildingTypeID BuildingTypeID;
        public AssetReferenceGameObject SpawnPlacePrefab;
        public List<Upgrade> Upgrades;
        public IBuildable BuildingType;
    }
}