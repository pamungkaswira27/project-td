using Pathfinding;
using StinkySteak.SimulationTimer;
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

        [Header("Multiple Max Random Position")]
        [SerializeField]
        private float _multipleForMaxRandomPosition;

        [Header("Animation")]
        [SerializeField]
        private Animator _animationPatrol;

        [Header("Timer for Stay in Place")]
        [SerializeField]
        private float _timer;

        [Header("Speed for Rotation")]
        [SerializeField]
        private float _rotationSpeed;

        private Path _path;
        private Seeker _seeker;
        private Vector3 _randomWayPoints;
        private Rigidbody _rigidBody;
        private SimulationTimer _stayTime;
        private int _currentWayPoint;
        private bool _nextStep;
        private bool _animate = true;
        private bool _isPatroling = true;
        [SerializeField]
        private float _minXRandomPoint;
        [SerializeField]
        private float _maxXRandomPoint;
        [SerializeField]
        private float _minZRandomPoint;
        [SerializeField]
        private float _maxZRandomPoint;

        public bool IsPatroling
        {
            get { return _isPatroling; }
            set { _isPatroling = value; }
        }

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidBody = GetComponent<Rigidbody>();
            _minXRandomPoint = transform.position.x;
            _maxXRandomPoint = transform.position.x * _multipleForMaxRandomPosition;
            _minZRandomPoint = transform.position.z;
            _maxZRandomPoint = transform.position.z * _multipleForMaxRandomPosition;
        }

        private void Start()
        {
            SetRandomWayPoints();
            _seeker.StartPath(_rigidBody.position, _randomWayPoints, OnPathComplete);
        }

        private void FixedUpdate()
        {
            if (_nextStep)
            {
                if (_stayTime.IsExpired())
                {
                    _nextStep = false;
                    _animate = true;
                    UpdatePath();
                    _stayTime = SimulationTimer.None;

                }
                return;
            }

            if (!_isPatroling)
            {
                this.enabled = false;
                return;
            }


            SetRandomWayPoints();
            Patrolling();
            Animation();



            if (_stayTime.IsRunning) return;
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
            Vector3 lookAtDir = (_path.vectorPath[_currentWayPoint] - transform.position);
            Vector3 force = _speedPatrolling * Time.deltaTime * forceDir;

            _rigidBody.velocity = force;
            Quaternion lookRotate = Quaternion.LookRotation(lookAtDir);
            transform.rotation = lookRotate;

            float toNextWay = Vector3.Distance(_rigidBody.position, _path.vectorPath[_currentWayPoint]);


            if (toNextWay <= _nextWayPoint)
            {
                _currentWayPoint++;

                if (_currentWayPoint == _path.vectorPath.Count)
                {
                    _nextStep = true;
                    _animate = false;
                    _stayTime = SimulationTimer.CreateFromSeconds(_timer);
                }
            }
        }

        private void Animation()
        {
            if (_animationPatrol == null) return;

            _animationPatrol.SetBool("IsMoving", _animate);
        }

        public void SetRandomWayPoints()
        {
            float nextStepX = Random.Range(_minXRandomPoint, _maxXRandomPoint);
            float nextStepZ = Random.Range(_minZRandomPoint, _maxZRandomPoint);
            _randomWayPoints = new Vector3(nextStepX, 0, nextStepZ);
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
