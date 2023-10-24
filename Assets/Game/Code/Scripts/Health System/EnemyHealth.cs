using UnityEngine;

namespace ProjectTD
{
    [RequireComponent (typeof (Damageable))]
    public class EnemyHealth : CharacterHealth
    {
        public override void DecreaseHealth(float amount)
        {
            base.DecreaseHealth(amount);

            if (_healthPoints <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
