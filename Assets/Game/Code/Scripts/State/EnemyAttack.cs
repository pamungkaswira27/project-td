using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public enum Attack
    {
        Patrol,
        Melee,
        Ranged,
        SelfExploding
    }

    public class EnemyAttack : MonoBehaviour
    {
        public Attack attackMode;

        private void Start()
        {
            attackMode = Attack.Patrol;
        }

        private void Update()
        {
            switch (attackMode)
            {
                case Attack.Patrol:
                    Debug.Log("Enemy Patroling.");
                    break;
                case Attack.Melee:
                    Debug.Log("attacked with Attack Melee.");
                    break;
                case Attack.Ranged:
                    Debug.Log("attacked with Attack Range.");
                    break;
                case Attack.SelfExploding:
                    Debug.Log("attacked with Attack Self-Exploding.");
                    break;
            }
        }
    }
}
