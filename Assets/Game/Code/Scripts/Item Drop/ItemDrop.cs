using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class ItemDrop : MonoBehaviour
    {
        public float damage;
        private IDamageable damageable;

        private void Start()
        {
            damageable = GetComponent<IDamageable>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // damageable.TakeDamage(damage);
            }
        }
    }
}
