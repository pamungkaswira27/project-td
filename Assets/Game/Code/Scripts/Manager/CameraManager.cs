using Cinemachine;
using UnityEngine;

namespace ProjectTD
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;

        [Header("Virtual Cameras")]
        [SerializeField]
        private CinemachineVirtualCamera _followCamera;

        private void Awake()
        {
            Instance = this;
        }

        public void SetupFollowCamera(Transform target)
        {
            _followCamera.m_Follow = target;
        }
    }
}
