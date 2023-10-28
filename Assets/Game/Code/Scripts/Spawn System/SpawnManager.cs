using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Rotation Face")]
        [SerializeField]
        protected Transform _facingEnemiesSpawn;

        protected ObjectPooler _objectPooler;

        protected virtual void SpawningEnemies()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        protected virtual void SpawningCrates()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        protected virtual IEnumerator SpawningTime()
        {
            yield return null;
        }
    }

}
