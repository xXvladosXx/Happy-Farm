using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.ProductionEntities.Settings
{
    [CreateAssetMenu(fileName = "ProductSettings", menuName = "StaticData/ProductSettings", order = 0)]
    public class ProductSettings : SerializedScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public IItem Item { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject ProductPrefab { get; private set; }
    }
}