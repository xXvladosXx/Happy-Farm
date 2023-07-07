using Codebase.Logic.Entity.ProductionEntities.Settings;
using Codebase.Logic.Storage.Container;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Logic.Entity.EnemyEntities.Settings
{
    [CreateAssetMenu(fileName = "EnemyAnimalSettings", menuName = "StaticData/EnemyAnimalSettings", order = 0)]
    public class EnemyAnimalSettings : SerializedScriptableObject
    {
        [field: SerializeField] public EnemyAnimalTypeID EnemyAnimalTypeID { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject AnimalPrefab { get; private set; }
        [field: SerializeField] public IItem Item { get; private set; }

        [ShowInInspector, TabGroup("Movement")] 
        public float IdleSpeed;

        [ShowInInspector, TabGroup("Movement")] 
        public float RunSpeed;

        [ShowInInspector, TabGroup("Catching")] 
        public float MaxTimeToWaitCaught;

        [ShowInInspector, TabGroup("Catching")] 
        public float TimeToCatch;

        [ShowInInspector, TabGroup("Catching")] 
        public int ClickAmountToCatch;
        
        [ShowInInspector, BoxGroup("Animation")]
        public AssetReference AnimatorStateHasher;
    }
}