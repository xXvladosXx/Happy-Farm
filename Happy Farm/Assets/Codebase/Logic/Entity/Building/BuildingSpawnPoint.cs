using UnityEngine;

namespace Codebase.Logic.Entity.Building
{
    public class BuildingSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        public Transform SpawnPoint => _spawnPoint;
    }
}