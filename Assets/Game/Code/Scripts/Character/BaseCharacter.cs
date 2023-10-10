using UnityEngine;

namespace ProjectTD
{
    public class BaseCharacter : MonoBehaviour
    {
        private void Start()
        {
            PlayerManager.Instance.SetupPlayer(this);
        }
    }
}
