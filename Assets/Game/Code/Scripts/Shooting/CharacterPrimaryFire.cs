using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class CharacterPrimaryFire : MonoBehaviour
    {
        [SerializeField]
        private Transform _firingPoint;
        [SerializeField]
        private float _firingRate;

        private ObjectPooler _objectPooler;
        private WaitForSeconds _firingRateWaitForSeconds;

        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
            _firingRateWaitForSeconds = new WaitForSeconds(1 / _firingRate);
        }

        public IEnumerator FireCoroutine()
        {
            while (true)
            {
                Fire();
                yield return _firingRateWaitForSeconds;
            }
        }

        private void Fire()
        {
            GameObject bullet = _objectPooler.GetPooledObject("Bullet", _firingPoint.position, _firingPoint.rotation);

            if (bullet != null)
            {
                bullet.GetComponent<Projectile>().SetShootDirection(_firingPoint.forward);
            }
        }
    }
}
