using NaughtyAttributes;
using Pathfinding;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent (typeof (Seeker), typeof (AIPath), typeof (AIDestinationSetter))]
    public class AIChase : MonoBehaviour
    {
        [SerializeField]
        private float _aIDetectionRadius;
        [SerializeField, Tag]
        private string _detectionTag;

        private AIDestinationSetter _aIDestinationSetter;
        private SphereCollider _detectionCollider;

        private void Awake()
        {
            _aIDestinationSetter = GetComponent<AIDestinationSetter>();
            _detectionCollider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            _detectionCollider.isTrigger = true;
            _detectionCollider.radius = _aIDetectionRadius;
        }

        private void Update()
        {
            if (_aIDestinationSetter.target != null)
            {
                if (transform.position == _aIDestinationSetter.target.position)
                {
                    _aIDestinationSetter.target = null;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_detectionTag))
            {
                _aIDestinationSetter.target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_detectionTag))
            {
                _aIDestinationSetter.target = transform;
            }
        }
    }
}
