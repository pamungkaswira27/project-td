using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProjectTD
{
    public class Settings : MonoBehaviour
    {
        [Header("--------- Audio Mixer ---------")]
        [SerializeField] private AudioMixer myMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider SFXSlider;
        [SerializeField] private AudioSource buttonClick;

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        }

        public void SetSFXVolume()
        {
            float volume = SFXSlider.value;
            myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        }

        public void playThisSoundButton()
        {
            buttonClick.Play();
        }

        private void Start()
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        void Back()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
