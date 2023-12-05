using UnityEngine;

namespace ProjectTD
{
    public class Barrel : MonoBehaviour
    {
        public void Explode()
        {
            ObjectPooler.Instance.GetPooledObject("ExplosionVFX", transform.position, Quaternion.identity);
        }
    }
}
