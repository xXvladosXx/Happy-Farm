using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "AnimalSettings", menuName = "StaticData/AnimalSettings", order = 0)]
    public class AnimalSettings : SerializedScriptableObject
    {
        [field: SerializeField] public AnimalTypeID AnimalTypeID { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject AnimalPrefab { get; private set; }
        [field: SerializeField] public IItem Item { get; private set; }
    }
}