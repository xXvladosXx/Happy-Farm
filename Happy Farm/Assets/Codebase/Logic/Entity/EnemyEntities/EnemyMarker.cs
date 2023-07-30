using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities
{
    public class EnemyMarker : MonoBehaviour
    {
        public EnemyAnimalTypeID BuildingTypeID;
        public float Time;
        public int SpawnCount;
        public bool IsLooped;
        public PortalParticleData PortalParticleData;
    }
}