using UnityEngine;

namespace ProjectTD
{
    public class StateManager : MonoBehaviour
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
        private GameObject _player; // ganti dengan value pada AI FoV
        [SerializeField]
        private GameObject _enemy; // ganti dengan value pada AI FoV
        [SerializeField]
        private AIFieldOfView _fieldOfView;
        //[SerializeField]
        //private LayerMask _obstructionMask; // ganti dengan value pada AI FoV



       // [SerializeField]
        private float _viewAngle; // ganti dengan value pada AI FoV
       // [SerializeField]
        private float _viewRadius; // ganti dengan value pada AI FoV

        private void Awake()
        {
            _attackMelee = GetComponent<EnemyMeleeAttack>();
            _attackRanged = GetComponent<EnemyRangedAttack>();
            _attackSelfExploding = GetComponent<EnemySelfExplodingAttack>();

            _obstructionMask = _fieldOfView.Obstruction;
            _viewAngle = _fieldOfView.Angle;
            _viewRadius = _fieldOfView.Radius;
        }

        void FixedUpdate()
        {
            _enemiesFacingDistance = _player.transform.position - _enemy.transform.position;
            _playerTarget = (_player.transform.position - _enemy.transform.position).normalized;
            _rangedDistance = Vector3.Distance(_player.transform.position, _enemy.transform.position);

            _inFOV = Physics.Raycast(_enemy.transform.position, _playerTarget, _rangedDistance, _obstructionMask);
            _inAngel = Vector3.Angle(_enemy.transform.forward, _playerTarget) < _viewAngle / 2;
            _inRangedDistance = _rangedDistance <= 15f && _rangedDistance > 3f && _rangedDistance <= _viewRadius && _lastRotation != _enemiesFacingDistance;
            _inMeleeDistance = _rangedDistance <= 2f && _rangedDistance > 1f && _rangedDistance <= _viewRadius && _lastRotation != _enemiesFacingDistance;
            _inSelfExplodingDistance = _rangedDistance <= 1.5f && _rangedDistance <= _viewRadius && _lastRotation != _enemiesFacingDistance;

            if (!_inAngel && _inFOV)
            {
                return;
            }

            if (_inAngel && _inRangedDistance && !_inFOV)
            {
                _attackRanged.RangedAttack(25);
                return;
            }

            if (_inAngel && _inMeleeDistance && !_inFOV)
            {
                //_attackMelee.MeleeAttack(15);
                return;
            }
            
            if (_inAngel && _inSelfExplodingDistance && !_inFOV)
            {
                _attackSelfExploding.SelfExplodingAttack(90);
                return;
            }


        }

    }
}
