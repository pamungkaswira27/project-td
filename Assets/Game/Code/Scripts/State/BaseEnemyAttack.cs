using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class BaseEnemyAttack : MonoBehaviour
    {
        protected const int COLLIDER_SIZE = 10;

        [SerializeField]
        protected AIFieldOfView aiFieldOfView;

        protected LayerMask _playerTarget;
        protected Collider[] _colliders;
        protected float _damage;

        protected LayerMask ObstructionMask => aiFieldOfView.Obstruction;
        protected float ViewAngle => aiFieldOfView.Angle;
        protected float ViewRadius => aiFieldOfView.Radius;

        private void Start()
        {
            Initialization();
        }

        public virtual void MeleeAttack(float damaged)
        {
            _damage = damaged; 
        }

        public virtual void RangedAttack(float damaged)
        {
            _damage = damaged;
        }

        public virtual void SelfExplodingAttack(float damage)
        {
            _damage = damage;
        }

        public virtual void Initialization()
        {
            // initialize somthing
        }

        public virtual IEnumerator IntervalAttack()
        {
            yield return null;
        }

        protected Transform GetTarget()
        {
            return aiFieldOfView.Target;
        }

        protected Transform LookAtPlayer()
        {
            return aiFieldOfView.transform;
        }

        protected string GetEnemyMeleeType()
        {
            return TagConst.TAG_ENEMY_MELEE;
        }
        
        protected string GetEnemyRangedType()
        {
            return TagConst.TAG_ENEMY_RANGED;
        }
        
        protected string GetEnemySelfExplodingType()
        {
            return TagConst.TAG_ENEMY_SELF_EXPLODING;
        }
    }
}
