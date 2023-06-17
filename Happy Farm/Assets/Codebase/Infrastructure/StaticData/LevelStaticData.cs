using UnityEngine;

namespace Codebase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public string LevelKey { get; set; }
        [field: SerializeField] public Vector3 PlayerSpawnPoint { get; set; }
    }
}