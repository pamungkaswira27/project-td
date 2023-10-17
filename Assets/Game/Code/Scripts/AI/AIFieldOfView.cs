using UnityEngine;

namespace ProjectTD
{
    public class AIFieldOfView : MonoBehaviour
    {
        [SerializeField]
        private float _radius;
        [SerializeField, Range(0f, 360f)]
        private float _angle;
        [SerializeField]
        private LayerMask _targetMask;
        [SerializeField]
        private LayerMask _obstructionMask;

        private Collider[] _targetColliders;
        private Transform _targetTransform;
        private Vector3 _directionToTarget;
        private bool _canSeePlayer;
        private float _distanceToTarget;

        public LayerMask Obstruction
        {
            get
            {
                return _obstructionMask;
            }
        }

        public Transform Target
        {
            get
            {
                return _targetTransform;
            }
        }

        public float Radius
        {
            get
            {
                return _radius;
            }
        }

        public float Angle
        {
            get
            {
                return _angle;
            }
        }

        public bool CanSeePlayer
        {
            get
            {
                return _canSeePlayer;
            }
        }

        private void FixedUpdate()
        {
            _targetColliders = Physics.OverlapSphere(transform.position, _radius, _targetMask);

            if (_targetColliders.Length <= 0)
            {
                _canSeePlayer = false;
                _targetTransform = null;
                return;
            }

            _targetTransform = _targetColliders[0].transform;
            _directionToTarget = (_targetTransform.position - transform.position).normalized;
            _canSeePlayer = true;

            if (Vector3.Angle(transform.forward, _directionToTarget) >= _angle / 2)
            {
                _canSeePlayer = false;
                _targetTransform = null;
                return;
            }

            if (Physics.Raycast(transform.position, _directionToTarget, _distanceToTarget, _obstructionMask))
            {
                _canSeePlayer = false;
                _targetTransform = null;
                return;
            }

            _distanceToTarget = Vector3.Distance(transform.position, _targetTransform.position);
        }
    }
}
