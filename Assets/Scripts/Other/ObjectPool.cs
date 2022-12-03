using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeLearningTest
{
    public class ObjectPool : MonoBehaviour
    {
        private GameObject _poolObj;
        private Transform _objParent;
        private Queue<GameObject> _objectQueue;
        
        public ObjectPool(GameObject objToSpawn, Transform objParent, int initialSize)
        {
            _poolObj = objToSpawn;
            _objParent = objParent;
            _objectQueue = new Queue<GameObject>(initialSize);
            ExpandPool(initialSize);
           

        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectQueue.Enqueue(obj);
        }


        private void ExpandPool(int expandBy)
        {
            for (int i = 0; i < expandBy; i++)
            {
                GameObject go = Instantiate(_poolObj, _objParent);
                go.SetActive(false);
                ReturnToPool(go);

            }
        }

        public GameObject RetrieveFromPool()
        {
            GameObject go;

            if (_objectQueue.Count > 0)
            {
                go = _objectQueue.Dequeue();
            }
            else
            {
                ExpandPool(5);
                go = RetrieveFromPool();
            }

            return go;
        }







    }
}
    
