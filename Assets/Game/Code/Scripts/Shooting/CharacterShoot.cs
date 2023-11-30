using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public abstract class CharacterShoot : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        protected Transform _firingPoint;

        [Header("Visual Effect")]
        [SerializeField]
        protected GameObject _muzzleFlashVFX;

        protected ObjectPooler _objectPooler;

        protected void Start()
        {
            Initialization();
        }

        private void Update()
        {
            OnUpdate();
        }

        protected virtual void Initialization()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        protected virtual void OnUpdate()
        {
            // Empty
        }

        public virtual IEnumerator FireCoroutine()
        {
            yield return null;
        }

        public virtual void PlayMuzzleFlashVFX()
        {
            if (_muzzleFlashVFX == null)
            {
                return;
            }

            _muzzleFlashVFX.SetActive(true);
        }

        public virtual void StopMuzzleFlashVFX()
        {
            if (_muzzleFlashVFX == null)
            {
                return;
            }

            _muzzleFlashVFX.SetActive(false);
        }
    }
}
