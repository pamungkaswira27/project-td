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

        public float RadiusPatrol
        {
            get { return _aiRadiusPatrol; }
        }

        public bool IsPatroling
        {
            get { return _isPatroling;}
            set { _isPatroling = value; }
        }

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SetRandomWayPoints();
            _seeker.StartPath(_rigidBody.position, _randomWayPoints, OnPathComplete);
        }

        private void Update()
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

            }

            if(!_isPatroling)
            {
                this.enabled = false;
            }

            if(_isPatroling)
            {
                SetRandomWayPoints();
                Patrolling();
                Animation();
            }


            if (_stayTime.IsRunning) return;
            _stayTime = SimulationTimer.CreateFromSeconds(_timer);
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

            _rigidBody.velocity = (force);
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
