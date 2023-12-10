using UnityEngine;
using UnityEngine.UI;

namespace ProjectTD
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField]
        private Slider _healthSlider;
        [SerializeField]
        private Slider _powerSlider;

        private void Start()
        {
            Invoke(nameof(Initialize), 0.1f);
        }

        private void Update()
        {
            UpdateHealthBar();
            UpdatePowerBar();
        }

        private void Initialize()
        {
            if (_healthSlider == null)
            {
                return;
            }

            if (_powerSlider == null)
            {
                return;
            }

            if (PlayerManager.Instance == null)
            {
                return;
            }

            _healthSlider.maxValue = PlayerManager.Instance.CharacterHealth.MaxHealthPoints;
            _healthSlider.value = PlayerManager.Instance.CharacterHealth.HealthPoints;

            _powerSlider.maxValue = PlayerManager.Instance.CharacterUltimateShoot.DurationTime;
            _powerSlider.value = PlayerManager.Instance.CharacterUltimateShoot.DurationTime;
        }

        private void UpdateHealthBar()
        {
            if (_healthSlider == null)
            {
                return;
            }

            _healthSlider.value = PlayerManager.Instance.CharacterHealth.HealthPoints;
        }

        private void UpdatePowerBar()
        {
            if (_powerSlider == null)
            {
                return;
            }

            if (PlayerManager.Instance.CharacterUltimateShoot.IsActive)
            {
                _powerSlider.maxValue = PlayerManager.Instance.CharacterUltimateShoot.DurationTime;
                _powerSlider.value = PlayerManager.Instance.CharacterUltimateShoot.DurationRemainTime;
                return;
            }
            
            if (PlayerManager.Instance.CharacterUltimateShoot.IsCooldown)
            {
                _powerSlider.maxValue = PlayerManager.Instance.CharacterUltimateShoot.CooldownTime;
                _powerSlider.value += Time.deltaTime;

                if (_powerSlider.value == _powerSlider.maxValue)
                {
                    PlayerManager.Instance.CharacterUltimateShoot.IsCooldown = false;
                    _powerSlider.value = _powerSlider.maxValue;
                }
            }
        }
    }
}
