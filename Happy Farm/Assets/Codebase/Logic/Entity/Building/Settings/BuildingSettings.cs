using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.Building.Settings
{
    [CreateAssetMenu(fileName = "BuildingSettings", menuName = "StaticData/BuildingSettings", order = 0)]
    public class BuildingSettings : SerializedScriptableObject
    {
        [field: SerializeField] public BuildingTypeID BuildingTypeID { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject BuildingPrefab { get; private set; }

        [ShowInInspector, TabGroup("Production")]
        public string Item;
        [ShowInInspector, TabGroup("Production")] 
        public float ProductionTime;
        [ShowInInspector, TabGroup("Production")]
        public float ProductionAmount;
        
        [ShowInInspector, TabGroup("Consumption")]
        public int ConsumptionAmount;
        [ShowInInspector, TabGroup("Consumption")]
        public string ConsumptionItem;
    }
}