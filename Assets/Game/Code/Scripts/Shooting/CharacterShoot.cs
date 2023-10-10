using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public abstract class CharacterShoot : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        protected Transform _firingPoint;

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
    }
}
