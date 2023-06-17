using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "BuildingSettings", menuName = "StaticData/BuildingSettings", order = 0)]
    public class BuildingSettings : SerializedScriptableObject
    {
        [field: SerializeField] public BuildingTypeID BuildingTypeID { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject BuildingPrefab { get; private set; }
    }
}