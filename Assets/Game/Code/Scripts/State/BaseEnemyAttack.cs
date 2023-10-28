using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class BaseEnemyAttack : MonoBehaviour
    {
        [SerializeField]
        protected AIFieldOfView aiFieldOfView;

        protected LayerMask ObstructionMask => aiFieldOfView.Obstruction;
        protected float ViewAngle => aiFieldOfView.Angle;
        protected float ViewRadius => aiFieldOfView.Radius;

        private int _damage;

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

        protected Transform GetTarget()
        {
            return aiFieldOfView.Target;
        }

        protected Transform LookAtPlayer()
        {
            return aiFieldOfView.transform;
        }
    }
}
