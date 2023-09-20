using StinkySteak.SimulationTimer;
using System.Collections;
using UnityEngine;

public class CharacterUltimateFire : MonoBehaviour
{
    [SerializeField]
    private Transform _firingPoint;
    [SerializeField]
    private int _numberOfLaser;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private float _intervalTimeBetweenLasers;

    private ObjectPooler _objectPooler;
    private SimulationTimer _cooldownTimer;
    private WaitForSeconds _intervalWaitForSeconds;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _cooldownTimer = SimulationTimer.None;
        _intervalWaitForSeconds = new WaitForSeconds(_intervalTimeBetweenLasers);
    }

    private void Update()
    {
        if (_cooldownTimer.IsExpired())
        {
            _cooldownTimer = SimulationTimer.None;
        }
    }

    public IEnumerator FireCoroutine()
    {
        if (!_cooldownTimer.IsRunning)
        {
            _cooldownTimer = SimulationTimer.CreateFromSeconds(_cooldown);

            for (int i = 0; i < _numberOfLaser; i++)
            {
                GameObject laser = _objectPooler.GetPooledObject("Laser", _firingPoint.position, _firingPoint.rotation);

                if (laser != null)
                {
                    laser.GetComponent<Projectile>().SetShootDirection(_firingPoint.forward);
                }

                yield return _intervalWaitForSeconds;
            }
        }
    }
}
