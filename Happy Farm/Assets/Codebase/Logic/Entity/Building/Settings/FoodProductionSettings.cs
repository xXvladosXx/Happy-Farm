using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.Building.Settings
{
    [CreateAssetMenu(fileName = "FoodProductionSettings", menuName = "StaticData/FoodProductionSettings", order = 0)]
    public class FoodProductionSettings : SerializedScriptableObject
    {
        public BuildingTypeID BuildingTypeID;
        public AssetReferenceGameObject BuildingPrefab;
        public float ProductionTime;
        public int ProductionAmount;
        public int Cost;
    }
}