using JSAM;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTD
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private Slider _musicVolumeSlider;
        [SerializeField]
        private Slider _soundVolumeSlider;

        private void Start()
        {
            InitializeVolumeSettings();
            InitializeVolumeSlider();
        }

        private void Update()
        {
            UpdateVolume();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("MusicVolume", AudioManager.MusicVolume);
            PlayerPrefs.SetFloat("SoundVolume", AudioManager.SoundVolume);

            PlayerPrefs.Save();
        }

        private void InitializeVolumeSettings()
        {
            AudioManager.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            AudioManager.SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

            SaveSettings();
        }

        private void InitializeVolumeSlider()
        {
            _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            _soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        }

        private void UpdateVolume()
        {
            AudioManager.MusicVolume = _musicVolumeSlider.value;
            AudioManager.SoundVolume = _soundVolumeSlider.value;
        }
    }
}
