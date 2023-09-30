using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class AIPatrol : MonoBehaviour
    {
        private float _waitTime;
        private float _distance;
        private bool _isReached;
        private Vector3 _Pos;
        private Vector3 _randomPos;
        private Vector3 _center;

        [Header("AI")]
        [SerializeField]
        private AIPath _aiPath;

        [Header("Game Objects, Transform, Vector 3")]
        [SerializeField]
        private GameObject[] _wayPoints;
        [SerializeField]
        private Transform _enemies;
        [SerializeField]
        private Transform _targetSpots;
        [SerializeField]
        private Vector3 _spawnPosition;

        [Header("Value of Time and Index")]
        [SerializeField]
        private float _startTime;
        [SerializeField]
        private int _wayPointIdx;

        private void Awake()
        {
            // time
            _waitTime = _startTime;

            // waypoints
            _wayPoints = GameObject.FindGameObjectsWithTag("Waypoints");
            _wayPointIdx = _wayPoints.Length - 1;
            _targetSpots.position = _wayPoints[_wayPointIdx].transform.position;

            // random spawn position
            _Pos = new Vector3(Random.Range(-_spawnPosition.x, _spawnPosition.x), 0, Random.Range(-_spawnPosition.z, _spawnPosition.z));
            _randomPos = _center + _Pos;
            _enemies.position = _randomPos;
        }

        private void FixedUpdate()
        {
            _distance = Vector3.Distance(_enemies.position, _targetSpots.position);
            _isReached = _distance <= _aiPath.endReachedDistance;

            if (_isReached)
            {
                if (_waitTime <= 0)
                {
                    _wayPointIdx += 1;
                    if (_wayPointIdx >= _wayPoints.Length)
                    {
                        _wayPointIdx = 0;
                    }
                    _targetSpots.position = _wayPoints[_wayPointIdx].transform.position;

                    _waitTime = _startTime;

                }
                else
                {
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }

}
