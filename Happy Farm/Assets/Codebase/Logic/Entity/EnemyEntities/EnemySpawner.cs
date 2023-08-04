using System.Collections;
using System.Collections.Generic;
using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.SceneManagement;
using Codebase.Infrastructure.StaticData;
using Codebase.Logic.Entity.ProductionEntities.Settings;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Logic.Entity.EnemyEntities
{
    public class EnemySpawner : IGameBehaviour
    {
        private readonly IGameFactory _gameFactory;
        private readonly EnemyAnimalTypeID _enemyAnimalTypeID;
        private readonly Vector3 _position;
        private readonly float _time;
        private readonly bool _isLooped;
        private readonly PortalParticleData _portalParticleData;
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _task;

        public EnemySpawner(IGameFactory gameFactory,
            EnemyAnimalTypeID enemyAnimalTypeID,
            Vector3 position,
            float time,
            bool isLooped,
            PortalParticleData portalParticleData,
            ICoroutineRunner coroutineRunner)
        {
            _gameFactory = gameFactory;
            _enemyAnimalTypeID = enemyAnimalTypeID;
            _position = position;
            _time = time;
            _isLooped = isLooped;
            _portalParticleData = portalParticleData;
            _coroutineRunner = coroutineRunner;
        }

        public void Initialize()
        {
            if(_task != null)
                _coroutineRunner.StopCoroutine(_task);

            _task = _coroutineRunner.StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(_time);
                yield return new WaitForSeconds(2);
                var start = Object.Instantiate(_portalParticleData.SpawnEffectStart, _position, Quaternion.identity);
                var startLifetimeConstant = start.main.startLifetime.constant;
                yield return new WaitForSeconds(startLifetimeConstant);
                var loop = Object.Instantiate(_portalParticleData.SpawnEffectIdle, _position, Quaternion.identity);
                Object.Destroy(start.gameObject, startLifetimeConstant);
                _gameFactory.CreateEnemyAnimal(_enemyAnimalTypeID, _position);
                yield return new WaitForSeconds(1);
                var end = Object.Instantiate(_portalParticleData.SpawnEffectClose, _position, Quaternion.identity);
                Object.Destroy(loop.gameObject);
                Object.Destroy(end.gameObject, end.main.startLifetime.constant);
                
                if (_isLooped) 
                    continue;
                
                break;
            }
        }

        public bool GameUpdate()
        {
            return true;
        }

        public void Recycle()
        {
            _coroutineRunner.StopCoroutine(_task);
            _task = null;
        }
    }
}