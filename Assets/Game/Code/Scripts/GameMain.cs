using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public static class GameMain
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Main()
        {
            CleanUpSingletons();

            //SpawnAudioManager();
        }

        private static void CleanUpSingletons() 
        {
            PlayerManager.Instance = null;
            InputManager.Instance = null;
            CheckpointManager.Instance = null;
            CameraManager.Instance = null;
            ObjectiveManager.Instance = null;
            ObjectPooler.Instance = null;
            RespawnerManager.Instance = null;
            DialogueManager.Instance = null;
        }

        //private static void SpawnAudioManager()
        //{
        //    GameObject audioManagerPrefab = Resources.Load<GameObject>(nameof(AudioManager));

        //    Object.Instantiate(audioManagerPrefab);

            
        //}
    }
}