using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class AttackState : MonoBehaviour
    {
        private EnemyMeleeAttack _attackMelee;
        private EnemyRangedAttack _attackRanged;
        private EnemySelfExplodingAttack _attackSelfExploding;
        private Vector3 _enemiesFacingDistance; // ganti dengan value pada AI FoV
        private Vector3 _lastRotation;
        private Vector3 _playerTarget; // ganti dengan value pada AI FoV
        private LayerMask _obstructionMask; // ganti dengan value pada AI FoV
        private float _rangedDistance;
        private bool _inAngel;
        private bool _inFOV;
        private bool _inRangedDistance;
        private bool _inMeleeDistance;
        private bool _inSelfExplodingDistance;

        [SerializeField]
        private AIFieldOfView _fieldOfView;

        private float _viewAngle;
        private float _viewRadius;

        private void Start()
        {
            _attackMelee = GetComponent<EnemyMeleeAttack>();
            _attackRanged = GetComponent<EnemyRangedAttack>();
            _attackSelfExploding = GetComponent<EnemySelfExplodingAttack>();

            _obstructionMask = _fieldOfView.Obstruction;
            _viewAngle = _fieldOfView.Angle;
            _viewRadius = _fieldOfView.Radius;

        }

        private void FixedUpdate()
        {
            if (_fieldOfView.Target != null)
            {
                _enemiesFacingDistance = transform.position - _fieldOfView.Target.position;
                _playerTarget = (_fieldOfView.Target.position - transform.position).normalized;
                _rangedDistance = Vector3.Distance(_fieldOfView.Target.position, transform.position);

                _inFOV = Physics.Raycast(transform.position, _playerTarget, _rangedDistance, _obstructionMask);
                _inAngel = Vector3.Angle(transform.forward, _playerTarget) < _viewAngle / 2;

                _inRangedDistance = _rangedDistance <= 15f && _rangedDistance > 3f && _rangedDistance <= _viewRadius;
                _inMeleeDistance = _rangedDistance <= 2f && _rangedDistance > 1f && _rangedDistance <= _viewRadius;
                _inSelfExplodingDistance = _rangedDistance <= 1.5f && _rangedDistance <= _viewRadius;


                if (_inAngel && _inRangedDistance && !_inFOV && _attackRanged != null)
                {
                    _attackRanged.RangedAttack(25);
                    return;
                }

                if (_inAngel && _inMeleeDistance && !_inFOV && _attackMelee != null)
                {
                    _attackMelee.MeleeAttack(15);
                    return;
                }

                if (_inAngel && _inSelfExplodingDistance && !_inFOV && _attackSelfExploding != null)
                {
                    _attackSelfExploding.SelfExplodingAttack(90);
                    return;
                }
            }
        }
    }
}
