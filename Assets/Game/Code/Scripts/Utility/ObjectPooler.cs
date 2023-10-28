using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;

        [SerializeField]
        private List<Pool> _poolList;

        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        [System.Serializable]
        public struct Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        private void Awake()
        {
            Instance = this;
            StartPooling();
        }

        public GameObject GetPooledObject(string poolTag, Vector3 positionToSpawn, Quaternion objectRotation)
        {
            if (!_poolDictionary.ContainsKey(poolTag))
            {
                #if UNITY_EDITOR
                Debug.LogWarning($"Pool with tag {poolTag} doesn't exist!");
                #endif
                return null;
            }

            GameObject objectToSpawn = _poolDictionary[poolTag].Dequeue();
            objectToSpawn.transform.position = positionToSpawn;
            objectToSpawn.transform.rotation = objectRotation;
            objectToSpawn.SetActive(true);

            _poolDictionary[poolTag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        private void StartPooling()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in _poolList)
            {
                GameObject objectPoolParentTransform = new GameObject(pool.tag);
                objectPoolParentTransform.transform.SetParent(transform);

                Queue<GameObject> objectPoolQueue = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject objectPool = Instantiate(pool.prefab, objectPoolParentTransform.transform);
                    objectPool.SetActive(false);
                    objectPoolQueue.Enqueue(objectPool);
                }

                _poolDictionary.Add(pool.tag, objectPoolQueue);
            }
        }
    }
}
