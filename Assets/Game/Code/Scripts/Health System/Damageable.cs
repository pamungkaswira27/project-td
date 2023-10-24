using System;
using UnityEngine;

namespace ProjectTD
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        public event Action<float> OnDamaged;

        public bool TryTakeDamage(float damagePoints)
        {
            OnDamaged?.Invoke(damagePoints);
            return true;
        }
    }
}
