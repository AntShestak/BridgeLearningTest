using System.Collections.Generic;
using UnityEngine;


namespace BridgeLearningTest {
    public class CollectibleController : MonoBehaviour
    {
        public static CollectibleController Instance
        {
            get;
            private set;
        }

        [SerializeField] private GameObject[] _collectiblePrefabs;
        [SerializeField] private SurfaceTileController _tiles;
        [SerializeField] private int _maxSpawn = 3;

        private Transform _collectibleParent;
        private Dictionary<CollectibleType, ObjectPool> _collectiblePools;
        

        private void Awake()
        {
            #region Singleton
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
            #endregion

            _collectibleParent = this.transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            //setup pools
            _collectiblePools = new Dictionary<CollectibleType, ObjectPool>();

            foreach (var obj in _collectiblePrefabs)
            {
                CollectibleType type = obj.GetComponent<CollectibleObject>().Type;
                ObjectPool pool = new ObjectPool(obj, _collectibleParent, 50);
                _collectiblePools.Add(type, pool);

            }
        }

        private void OnEnable()
        {
            GameController.OnLevelStart += SpawnCollectibles;
            
        }


        private void OnDisable()
        {
            GameController.OnLevelStart -= SpawnCollectibles;
            
        }

        public void CollectibleCollected(CollectibleObject obj)
        {
            ReturnCollectible(obj);
            SpawnCollectibles();
            ScoreController.Instance.AddScore(obj.Type);
        }

        public void ReturnCollectible(CollectibleObject obj)
        {
            CollectibleType type = obj.Type;
            _collectiblePools[type].ReturnToPool(obj.gameObject);

            //return tile
            _tiles.ReturnOccupiedTile(obj.transform.position);

        }

        public void SpawnCollectibles()
        {
            int numberToSpawn = Random.Range(1, _maxSpawn);

            for (int i = 0; i < numberToSpawn; i++)
            {
                Vector3? spawnPosition = _tiles.GetRandomPosAndOccupyTile();
                if (spawnPosition == null) return;

                CollectibleType randomType = (Random.Range(0, 100) > 50) ? CollectibleType.Sphere : CollectibleType.Capsule;
                //Instantiate(go, (Vector3)spawnPosition, Quaternion.identity, _collectibleParent);

                GameObject go = _collectiblePools[randomType].RetrieveFromPool();
                go.transform.position = (Vector3)spawnPosition;
                go.SetActive(true);
            }
        }
    }
}

