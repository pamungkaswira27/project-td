using TMPro;
using UnityEngine;

namespace ProjectTD
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _messageText;

        private void Update()
        {
            UpdateMessageText();
        }

        private void UpdateMessageText()
        {
            _messageText.text = DialogueManager.Instance.Sentence;
        }
    }
}
