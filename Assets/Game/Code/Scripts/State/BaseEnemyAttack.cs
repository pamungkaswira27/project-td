using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class BaseEnemyAttack : MonoBehaviour
    {
        private int _damage;
        [SerializeField]
        protected AIFieldOfView aiFieldOfView;

        public virtual void MeleeAttack(int damaged)
        {
            _damage = damaged;
        }

        public virtual void RangedAttack(int damaged)
        {
            _damage = damaged;
        }

        public virtual void SelfExplodingAttack(int damage)
        {
            _damage = damage;
        }
    }
}
