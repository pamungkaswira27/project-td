using StinkySteak.SimulationTimer;
using UnityEngine;

namespace ProjectTD
{
    public class VFXDisabler : MonoBehaviour
    {
        [Tooltip("Assign with the longest VFX duration")]
        [SerializeField]
        private ParticleSystem _vFX;

        private SimulationTimer _disableTimer;

        private void OnEnable()
        {
            _disableTimer = SimulationTimer.CreateFromSeconds(_vFX.main.duration);
        }

        private void Update()
        {
            if (_disableTimer.IsExpired())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
