using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class CharacterBasicShoot : CharacterShoot
    {
        [Header("Basic Shoot")]
        [SerializeField]
        private float _firingRate = 15f;

        private GameObject _basicProjectile;
        private WaitForSeconds _firingRateWaitForSeconds;

        protected override void Initialization()
        {
            base.Initialization();
            _firingRateWaitForSeconds = new WaitForSeconds(1 / _firingRate);
        }

        public override IEnumerator FireCoroutine()
        {
            while (true)
            {
                _basicProjectile = _objectPooler.GetPooledObject("BasicProjectile", _firingPoint.position, _firingPoint.rotation);

                if (_basicProjectile.TryGetComponent(out BasicProjectile basicProjectile))
                {
                    basicProjectile.SetProjectileDirection(_firingPoint.forward);
                }

                yield return _firingRateWaitForSeconds;
            }
        }
    }
}
