using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities
{
    public class EnemySpawner 
    {
        private readonly IGameFactory _gameFactory;
        private readonly EnemyAnimalTypeID _enemyAnimalTypeID;
        private readonly Vector3 _position;
        private readonly float _time;
        private readonly bool _isLooped;
        private readonly PortalParticleData _portalParticleData;

        public EnemySpawner(IGameFactory gameFactory,
            EnemyAnimalTypeID enemyAnimalTypeID,
            Vector3 position,
            float time,
            bool isLooped,
            PortalParticleData portalParticleData)
        {
            _gameFactory = gameFactory;
            _enemyAnimalTypeID = enemyAnimalTypeID;
            _position = position;
            _time = time;
            _isLooped = isLooped;
            _portalParticleData = portalParticleData;
        }

        public async void SpawnEnemy()
        {
            while (true)
            {
                await UniTask.Delay((int) (_time * 1000));
                await UniTask.Delay(2000);
                var start = Object.Instantiate(_portalParticleData.SpawnEffectStart, _position, Quaternion.identity);
                var startLifetimeConstant = start.main.startLifetime.constant;
                await UniTask.Delay((int) (startLifetimeConstant*1000));
                var loop = Object.Instantiate(_portalParticleData.SpawnEffectIdle, _position, Quaternion.identity);
                Object.Destroy(start.gameObject, startLifetimeConstant);
                await _gameFactory.CreateEnemyAnimal(_enemyAnimalTypeID, _position);
                await UniTask.Delay(1000);
                var end = Object.Instantiate(_portalParticleData.SpawnEffectClose, _position, Quaternion.identity);
                Object.Destroy(loop.gameObject);
                Object.Destroy(end.gameObject, end.main.startLifetime.constant);
                
                if (_isLooped) 
                    continue;
                
                break;
            }
        }
    }
}