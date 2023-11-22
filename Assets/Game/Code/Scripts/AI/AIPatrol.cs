using Pathfinding;
using UnityEngine;

namespace ProjectTD
{
    public class AIPatrol : MonoBehaviour
    {
        [Header("AI Patrol")]
        [SerializeField]
        private float _nextWayPoint;
        [SerializeField]
        private float _speedPatrolling;

        [Header("Radius Patrolling")]
        [SerializeField]
        private float _aiRadiusPatrol;

        [Header("Range for Random Position")]
        [SerializeField]
        private int _minXRandomPoint;
        [SerializeField]
        private int _maxXRandomPoint;
        [SerializeField]
        private int _minZRandomPoint;
        [SerializeField]
        private int _maxZRandomPoint;

        private Path _path;
        private Seeker _seeker;
        private Rigidbody _rigidBody;
        private Vector3 _randomWayPoints;
        private int _currentWayPoint;
        private float _timer;
        private bool _nextStep;

        public float RadiusPatrol
        {
            get
            {
                return _aiRadiusPatrol;
            }
        }

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _timer = 2f;
            SetRandomWayPoints();
            _seeker.StartPath(_rigidBody.position, _randomWayPoints, OnPathComplete);
        }

        private void FixedUpdate()
        {
            Patrolling();

            if (_nextStep)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = 2f;
                    _nextStep = false;
                    UpdatePath();
                }
            }
        }

        private void UpdatePath()
        {
            if (_seeker.IsDone())
            {
                SetRandomWayPoints();
                _seeker.StartPath(_rigidBody.position, _randomWayPoints, OnPathComplete);
            }
        }

        private void Patrolling()
        {
            if (_path == null) return;
            if (_currentWayPoint >= _path.vectorPath.Count) return;


            Vector3 forceDir = (_path.vectorPath[_currentWayPoint] - _rigidBody.position);
            Vector3 force = _speedPatrolling * forceDir;
            _rigidBody.AddForce(force);

            float toNextWay = Vector3.Distance(_rigidBody.position, _path.vectorPath[_currentWayPoint]);


            if (toNextWay <= _nextWayPoint)
            {
                _currentWayPoint++;

                if (_currentWayPoint == _path.vectorPath.Count)
                {
                    _nextStep = true;
                }
            }
        }

        private void SetRandomWayPoints()
        {
            Vector2 nexStep = Random.insideUnitCircle.normalized * _aiRadiusPatrol;
            _randomWayPoints = new Vector3(nexStep.x, transform.position.y, nexStep.y) + transform.position;
        }

        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _path = path;
                _currentWayPoint = 0;
            }
        }
    }

}
