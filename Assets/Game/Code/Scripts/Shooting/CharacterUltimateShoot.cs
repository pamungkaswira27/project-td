using JSAM;
using StinkySteak.SimulationTimer;
using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class CharacterUltimateShoot : CharacterShoot
    {
        [Header("Ultimate Shoot")]
        [SerializeField]
        private int _numberOfProjectile = 3;
        [SerializeField]
        private float _duration = 10f;
        [SerializeField]
        private float _cooldown = 60f;
        [SerializeField]
        private float _intervalBetweenShoot = 0.5f;
        [SerializeField]
        private float _intervalBetweenProjectile = 0.1f;

        private GameObject _ultimateProjectile;
        private SimulationTimer _durationTimer;
        private SimulationTimer _cooldownTimer;
        private SimulationTimer _intervalBetweenShootTimer;
        private WaitForSeconds _intervalBetweenProjectileWaitForSeconds;

        protected override void Initialization()
        {
            base.Initialization();

            _durationTimer = SimulationTimer.None;
            _cooldownTimer = SimulationTimer.None;
            _intervalBetweenShootTimer = SimulationTimer.None;
            _intervalBetweenProjectileWaitForSeconds = new WaitForSeconds(_intervalBetweenProjectile);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (_durationTimer.IsExpired())
            {
                _durationTimer = SimulationTimer.None;
                _cooldownTimer = SimulationTimer.CreateFromSeconds(_cooldown);
            }

            if (_cooldownTimer.IsExpired())
            {
                _cooldownTimer = SimulationTimer.None;
            }

            if (_intervalBetweenShootTimer.IsExpired())
            {
                _intervalBetweenShootTimer = SimulationTimer.None;
            }
        }

        public override IEnumerator FireCoroutine()
        {
            if (!_intervalBetweenShootTimer.IsRunning)
            {
                _intervalBetweenShootTimer = SimulationTimer.CreateFromSeconds(_intervalBetweenShoot);

                for (int i = 0; i < _numberOfProjectile; i++)
                {
                    AudioManager.PlaySound(MainSounds.player_shoot_effect);
                    _ultimateProjectile = _objectPooler.GetPooledObject("UltimateProjectile", _firingPoint.position, _firingPoint.rotation);

                    if (_ultimateProjectile.TryGetComponent(out UltimateProjectile ultimateProjectile))
                    {
                        ultimateProjectile.SetProjectileDirection(_firingPoint.forward);
                    }

                    yield return _intervalBetweenProjectileWaitForSeconds;
                }

                StopMuzzleFlashVFX();
            }
        }

        public bool IsUltimateActive()
        {
            return _durationTimer.IsRunning;
        }

        public void ActivateUltimate()
        {
            if (!_cooldownTimer.IsRunning && !_durationTimer.IsRunning)
            {
                AudioManager.PlaySound(MainSounds.ultimate_shot_ready);
                _durationTimer = SimulationTimer.CreateFromSeconds(_duration);
            }
        }
    }
}
