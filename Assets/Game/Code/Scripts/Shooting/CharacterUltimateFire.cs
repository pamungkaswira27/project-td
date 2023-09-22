using StinkySteak.SimulationTimer;
using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class CharacterUltimateFire : MonoBehaviour
    {
        [SerializeField]
        private Transform _firingPoint;
        [SerializeField]
        private int _numberOfLaser;
        [SerializeField]
        private float _cooldown;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private float _burstInterval;
        [SerializeField]
        private float _intervalBetweenLasers;

        private ObjectPooler _objectPooler;
        private SimulationTimer _cooldownTimer;
        private SimulationTimer _durationTimer;
        private SimulationTimer _burstIntervalTimer;
        private WaitForSeconds _intervalBetweenLasersWaitForSeconds;

        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
            _cooldownTimer = SimulationTimer.None;
            _durationTimer = SimulationTimer.None;
            _burstIntervalTimer = SimulationTimer.None;
            _intervalBetweenLasersWaitForSeconds = new WaitForSeconds(_intervalBetweenLasers);
        }

        private void Update()
        {
            if (_durationTimer.IsExpired())
            {
                _durationTimer = SimulationTimer.None;
                _cooldownTimer = SimulationTimer.CreateFromSeconds(_cooldown);
            }

            if (_cooldownTimer.IsExpired())
            {
                _cooldownTimer = SimulationTimer.None;
            }

            if (_burstIntervalTimer.IsExpired())
            {
                _burstIntervalTimer = SimulationTimer.None;
            }
        }

        public void ActivateUltimate()
        {
            if (!_cooldownTimer.IsRunning && !_durationTimer.IsRunning)
            {
                _durationTimer = SimulationTimer.CreateFromSeconds(_duration);
            }
        }

        public bool IsUltimateActive()
        {
            return _durationTimer.IsRunning;
        }

        public IEnumerator FireCoroutine()
        {
            if (!_burstIntervalTimer.IsRunning)
            {
                _burstIntervalTimer = SimulationTimer.CreateFromSeconds(_burstInterval);

                for (int i = 0; i < _numberOfLaser; i++)
                {
                    GameObject laser = _objectPooler.GetPooledObject("Laser", _firingPoint.position, _firingPoint.rotation);

                    if (laser != null)
                    {
                        laser.GetComponent<Projectile>().SetShootDirection(_firingPoint.forward);
                    }

                    yield return _intervalBetweenLasersWaitForSeconds;
                }
            }
        }
    }
}
