using System.Collections;
using UnityEngine;
using System;

namespace BridgeLearningTest
{
    public class ObstacleController : MonoBehaviour
    {

        public static event Action OnObstacleSpawn;

        [SerializeField] private SurfaceTileController _tileController;
        [SerializeField] private GameObject _obstaclePrefab;
        
        [SerializeField] private float _obstacleSpawnTimeMax = 5f;
        [SerializeField] private float _obstacleSpawnTimeMin = 2f;

        private ObjectPool _pool;
        private Transform _obstacleParent;
        private IEnumerator _spawnerCoroutine;
        private float _nextSpawnTime = 0;
        private float _spawnTimer = 0;

        private void Awake()
        {
            _obstacleParent = this.transform;
        }

        private void Start()
        {
            _pool = new ObjectPool(_obstaclePrefab, _obstacleParent, 25);
            _spawnerCoroutine = ObstacleSpawnerCoroutine();
        }

        private void OnEnable()
        {
            GameController.OnLevelStart += StartSpawning;
            GameController.OnLevelEnd += StopSpawning;
        }

        private void OnDisable()
        {
            GameController.OnLevelStart -= StartSpawning;
            GameController.OnLevelEnd -= StopSpawning;
        }

        private void StartSpawning()
        {
            StartCoroutine(_spawnerCoroutine);
        }

        private void StopSpawning()
        {
            StopCoroutine(_spawnerCoroutine);
        }

        private void SpawnObstacle()
        {
            Vector3? spawnPosition = _tileController.GetRandomPosAndBlockTile();
            if (spawnPosition == null) return;

            //Instantiate(_obstaclePrefab, (Vector3)spawnPosition, Quaternion.identity, _obstacleParent);
            GameObject obstacle = _pool.RetrieveFromPool();
            obstacle.transform.position = (Vector3)spawnPosition;
            obstacle.SetActive(true);

            OnObstacleSpawn?.Invoke();

        }

        private float GetRandomSpawnTime()
        {
            return UnityEngine.Random.Range(_obstacleSpawnTimeMin, _obstacleSpawnTimeMax);
        }

        IEnumerator ObstacleSpawnerCoroutine()
        {
            
            while (true)
            {
                _nextSpawnTime = GetRandomSpawnTime();
                yield return new WaitForSeconds(_nextSpawnTime);
                SpawnObstacle();
                
            }
           
        }
    }
}