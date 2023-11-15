using JSAM;
using UnityEngine;

namespace ProjectTD.Prototyping
{
    /// <summary>
    /// A simple behaviour to test audio on behaviour enabled
    /// </summary>
    public class OnEnableAudio : MonoBehaviour
    {
        [SerializeField] private MainSounds _audio;

        private void OnEnable()
        {
            AudioManager.PlaySound(_audio);
        }
    }
}