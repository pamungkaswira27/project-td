using System;
using TMPro;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent(typeof(Damageable))]
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField]
        private AIAlertSystem _aiAlert;

        public override void DecreaseHealth(float amount)
        {
            base.DecreaseHealth(amount);

            if (_healthPoints <= 0f)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _aiAlert.OnAttacked();
            }
        }
    }
}
