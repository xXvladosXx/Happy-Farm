using System.Collections.Generic;
using Codebase.Infrastructure.Factory;
using Codebase.Logic.Entity.Building.Constructions;
using Codebase.Logic.Upgrades;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.Building.Settings.SpawnPlace
{
    [CreateAssetMenu(fileName = "SpawnPlaceBuildingSettings", menuName = "StaticData/SpawnPlaceBuildingSettings")]
    public abstract  class SpawnPlaceBuildingSettings : SerializedScriptableObject
    {
        public BuildingTypeID BuildingTypeID;
        public AssetReferenceGameObject SpawnPlacePrefab;
        public List<Upgrade> Upgrades;

        public abstract IBuildable CreateBuilding(IGameFactory gameFactory);
    }
}