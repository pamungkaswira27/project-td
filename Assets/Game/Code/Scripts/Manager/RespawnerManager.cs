using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class RespawnerManager : MonoBehaviour
    {
        public static RespawnerManager Instance;

        [SerializeField]
        private List<Respawner> _objectToRespawnList;
        [SerializeField]

        private void Awake()
        {
            Instance = this;
        }

        public void AddObjectToRespawn(Respawner objectToRespawn)
        {
            _objectToRespawnList.Add(objectToRespawn);
        }

        public void RespawnObjects()
        {
            foreach (Respawner objectToRespawn in _objectToRespawnList)
            {
                objectToRespawn.Respawn();
            }
        }

        public void ClearList()
        {
            foreach (Respawner objectToRespawn in _objectToRespawnList.ToArray())
            {
                if (!objectToRespawn.gameObject.activeInHierarchy)
                {
                    _objectToRespawnList.Remove(objectToRespawn);
                }
            }
        }
    }
}
