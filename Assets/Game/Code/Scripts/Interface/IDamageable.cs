using System;

namespace ProjectTD
{
    public interface IDamageable
    {
        public event Action<float> OnDamaged;
        public bool TryTakeDamage(float damagePoints);
    }
}
