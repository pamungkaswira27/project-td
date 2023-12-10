using Pathfinding;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent(typeof(AIFieldOfView), typeof(Rigidbody), typeof(Seeker))]
    public class AIChase : MonoBehaviour
    {
        [SerializeField]
        private AIFieldOfView _aIFieldOfView;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _nextWaypointDistance = 3f;
        [SerializeField]
        private Animator _enemyAnim;

        //private AIPatrol _patrol;
        private AIAlertSystem _alertSystem;
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
            //_patrol = GetComponent<AIPatrol>();
            _alertSystem = GetComponent<AIAlertSystem>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);
        }

        private void Update()
        {
            if (_currentPath != null)
            {
                if (_currentWaypointIndex >= _currentPath.vectorPath.Count)
                {
                    return;
                }

                if (_aIFieldOfView.Target == null)
                {
                    _currentPath = null;
                    _alertSystem.NotAttacked();
                    return;
                }

                _enemyAnim.SetBool("IsChasingPlayer", true);
                //_patrol.IsPatroling = false;
                _forceDirection = (_currentPath.vectorPath[_currentWaypointIndex] - _rigidbody.position);
                _rigidbodyForce = _speed * Time.deltaTime * _forceDirection;
                _rigidbody.velocity = (_rigidbodyForce);


                _lookAtDirection = (_aIFieldOfView.Target.position - transform.position);
                transform.localRotation = Quaternion.LookRotation(_lookAtDirection);

                _distanceToWaypoint = Vector3.Distance(_rigidbody.position, _currentPath.vectorPath[_currentWaypointIndex]);

                if (_distanceToWaypoint < _nextWaypointDistance)
                {
                    _currentWaypointIndex++;
                    return;
                }
                return;
            }
            _enemyAnim.SetBool("IsChasingPlayer", false);
            //_patrol.IsPatroling = true;
            //_patrol.enabled = true;
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
