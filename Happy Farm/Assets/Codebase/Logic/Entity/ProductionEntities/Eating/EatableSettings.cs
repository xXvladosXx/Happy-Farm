using System;
using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.ProductionEntities.Eating
{
    [Serializable]
    public struct EatableHealthData
    {
        public float Health;
        public float HealthDecrease;
        public float HealthDecreaseInterval;
    }

    [CreateAssetMenu(fileName = "EatableSettings", menuName = "StaticData/EatableSettings", order = 0)]
    public class EatableSettings: SerializedScriptableObject
    {
        public EatableTypeID EatableTypeID;
        public EatableHealthData EatableHealthData;
        public AssetReferenceGameObject Prefab;
    }
}