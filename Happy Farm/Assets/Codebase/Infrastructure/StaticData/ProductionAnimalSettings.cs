using Codebase.Logic.Animations;
using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "AnimalSettings", menuName = "StaticData/AnimalSettings", order = 0)]
    public class ProductionAnimalSettings : SerializedScriptableObject
    {
        [field: SerializeField] public ProductionAnimalTypeID ProductionAnimalTypeID { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject AnimalPrefab { get; private set; }
        [field: SerializeField] public IItem Item { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
       
        [ShowInInspector, TabGroup("Movement")] 
        public float IdleSpeed;

        [ShowInInspector, TabGroup("Movement")] 
        public float RunSpeed;

        [ShowInInspector, TabGroup("Eating")] 
        public float HungerThreshold;

        [ShowInInspector, TabGroup("Eating")] 
        public float EatingRate;
        
        [ShowInInspector, TabGroup("Eating")]
        public float EatingAmount;
        
        [ShowInInspector, TabGroup("Eating")] 
        public float HungerRate;

        [ShowInInspector, TabGroup("Eating")]
        public float HungerAmount;

        [ShowInInspector, TabGroup("Production")]
        public string ProductionName;

        [ShowInInspector, BoxGroup("Animation")]
        public AssetReference AnimatorStateHasher;
    }
}