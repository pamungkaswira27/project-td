using UnityEngine;

namespace ProjectTD
{
    public class Item : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.TryGetComponent<BaseCharacter>(out _))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
