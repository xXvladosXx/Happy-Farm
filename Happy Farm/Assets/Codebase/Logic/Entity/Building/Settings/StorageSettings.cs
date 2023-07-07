using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.Building.Settings
{
    [CreateAssetMenu(fileName = "StorageSettings", menuName = "StaticData/StorageSettings", order = 0)]
    public class StorageSettings : SerializedScriptableObject
    {
        public BuildingTypeID BuildingTypeID;
        public AssetReferenceGameObject BuildingPrefab;
        public int Capacity;
    }
}