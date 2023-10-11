using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class StateManager : MonoBehaviour
    {
        private Vector3 _enemiesFacingDistance;
        private Vector3 _lastRotation;
        private Vector3 _playerTarget;
        private float _rangedDistance;
        private bool _inAngel;
        private bool _inFOV;
        private bool _inRangedDistance;
        private bool _inMeleeDistance;
        private bool _inSelfExplodingDistance;

        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private GameObject _enemy;
        [SerializeField]
        private LayerMask _obstructionMask;
        [SerializeField]
        private EnemyAttack _attackEnemy;

        [SerializeField]
        private float _viewAngle;
        [SerializeField]
        private float _viewRadius;

        void Update()
        {
            _enemiesFacingDistance = _player.transform.position - _enemy.transform.position;
            _playerTarget = (_player.transform.position - _enemy.transform.position).normalized;
            _rangedDistance = Vector3.Distance(_player.transform.position, _enemy.transform.position);

            _inFOV = Physics.Raycast(_enemy.transform.position, _playerTarget, _rangedDistance, _obstructionMask);
            _inAngel = Vector3.Angle(_enemy.transform.forward, _playerTarget) < _viewAngle / 2;
            _inRangedDistance = _rangedDistance <= 15 && _rangedDistance > 3 && _rangedDistance <= _viewRadius && _lastRotation != _enemiesFacingDistance;
            _inMeleeDistance = _rangedDistance <= 2f && _rangedDistance <= _viewRadius && _lastRotation != _enemiesFacingDistance;
            _inSelfExplodingDistance = _rangedDistance <= 1.5f && _rangedDistance <= _viewRadius && _lastRotation != _enemiesFacingDistance;

            if (!_inAngel && _inFOV)
            {
                return;
            }

            if (_inAngel && _inRangedDistance && !_inFOV)
            {
                RangedAttackMode();
                return;
            }

            if (_inAngel && _inMeleeDistance && !_inFOV)
            {
                MeleeAttackMode();
                return;
            }
            
            if (_inAngel && _inSelfExplodingDistance && !_inFOV)
            {
                SelfExplodingAttackMode();
                return;
            }


        }

        private void RangedAttackMode()
        {
            _attackEnemy.attackMode = Attack.Ranged;
            _enemy.transform.LookAt(_player.transform.position);
            _lastRotation = _enemiesFacingDistance;
        }

        private void MeleeAttackMode()
        {
            _attackEnemy.attackMode = Attack.Melee;
            _enemy.transform.LookAt(_player.transform.position);
            _lastRotation = _enemiesFacingDistance;
        }

        private void SelfExplodingAttackMode()
        {
            _attackEnemy.attackMode = Attack.SelfExploding;
            _enemy.transform.LookAt(_player.transform.position);
        }

    }
}
