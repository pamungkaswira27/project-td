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
            Initialize();
        }

        private void Update()
        {
            UpdateHealthBar();
            UpdatePowerBar();
        }

        private void Initialize()
        {
            IsRequiredComponentNull();

            _healthSlider.maxValue = PlayerManager.Instance.CharacterHealth.MaxHealthPoints;
            _healthSlider.value = PlayerManager.Instance.CharacterHealth.HealthPoints;

            _powerSlider.maxValue = 0f;
            _powerSlider.value = 0f;
        }

        private void UpdateHealthBar()
        {
            IsRequiredComponentNull();

            _healthSlider.value = PlayerManager.Instance.CharacterHealth.HealthPoints;
        }

        private void UpdatePowerBar()
        {
            IsRequiredComponentNull();

            _powerSlider.value = 0f;
        }

        private bool IsRequiredComponentNull()
        {
            return (_healthSlider == null || _powerSlider == null || PlayerManager.Instance == null) ;
        }
    }
}
