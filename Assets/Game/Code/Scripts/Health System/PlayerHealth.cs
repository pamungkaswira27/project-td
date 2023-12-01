using JSAM;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerHealth : CharacterHealth
    {
        public override void DecreaseHealth(float amount)
        {
            AudioManager.PlaySound(MainSounds.player_hit_effect_01);
            base.DecreaseHealth(amount);

            if (_healthPoints <= 0f)
            {
                InputManager.Instance.DisablePlayerInput();
                _healthPoints = 0f;
                PlayerManager.Instance.DecreaseLife();
                gameObject.SetActive(false);
            }
        }
    }
}
