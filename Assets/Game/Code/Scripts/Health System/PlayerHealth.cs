using UnityEngine;

namespace ProjectTD
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerHealth : CharacterHealth
    {
        public override void DecreaseHealth(float amount)
        {
            base.DecreaseHealth(amount);

            if (_healthPoints <= 0f)
            {
                _healthPoints = 0f;
                PlayerManager.Instance.DecreaseLife();
                gameObject.SetActive(false);
            }
        }
    }
}
