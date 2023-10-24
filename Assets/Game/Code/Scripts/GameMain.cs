using UnityEngine;

namespace ProjectTD
{
    public static class GameMain
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Main()
        {
            CleanUpSingletons();
        }

        private static void CleanUpSingletons() 
        {
            PlayerManager.Instance = null;
            InputManager.Instance = null;
            CheckpointManager.Instance = null;
            CameraManager.Instance = null;
            ObjectPooler.Instance = null;
        }
    }
}