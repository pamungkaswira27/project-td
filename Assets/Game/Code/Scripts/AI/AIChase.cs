using Pathfinding;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent (typeof (AIFieldOfView), typeof(Rigidbody), typeof(Seeker))]
    public class AIChase : MonoBehaviour
    {
        [SerializeField]
        private AIFieldOfView _aIFieldOfView;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _nextWaypointDistance = 3f;

        private Path _currentPath;
        private Seeker _seeker;
        private Rigidbody _rigidbody;
        private Vector3 _lookAtDirection;
        private Vector3 _forceDirection;
        private Vector3 _rigidbodyForce;
        private Quaternion _targetRotation;
        private int _currentWaypointIndex;
        private float _distanceToWaypoint;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);
        }

        private void FixedUpdate()
        {
            if (_currentPath != null)
            {
                if (_currentWaypointIndex >= _currentPath.vectorPath.Count)
                {
                    return;
                }

                if (_aIFieldOfView.Target == null)
                {
                    return;
                }

                _forceDirection = (_currentPath.vectorPath[_currentWaypointIndex] - _rigidbody.position).normalized;
                _rigidbodyForce = _speed * _forceDirection;
                _rigidbody.AddForce(_rigidbodyForce);

                _lookAtDirection = (_aIFieldOfView.Target.position - transform.position).normalized;
                transform.localRotation = Quaternion.LookRotation(_lookAtDirection);

                _distanceToWaypoint = Vector3.Distance(_rigidbody.position, _currentPath.vectorPath[_currentWaypointIndex]);

                if (_distanceToWaypoint < _nextWaypointDistance)
                {
                    _currentWaypointIndex++;
                }
            }
        }

        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _currentPath = path;
                _currentWaypointIndex = 0;
            }
        }

        private void UpdatePath()
        {
            if (_aIFieldOfView.Target != null)
            {
                if (_seeker.IsDone())
                {
                    _seeker.StartPath(_rigidbody.position, _aIFieldOfView.Target.position, OnPathComplete);
                }
            }
        }
    }
}
