using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [SerializeField]
        private float _dialogueDisplayTime;

        private Queue<string> _sentenceQueue;

        private string _sentence;
        private bool _dialogueCompleted;

        public string Sentence => _sentence;
        public bool DialogueCompleted => _dialogueCompleted;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _sentenceQueue = new Queue<string>();
        }

        public void StartDialogue(DialogueSO dialogue)
        {
            InputManager.Instance.DisablePlayerInput();
            _dialogueCompleted = false;

            _sentenceQueue.Clear();

            for (int i = 0; i < dialogue.GetSentenceLength(); i++)
            {
                _sentenceQueue.Enqueue(dialogue.GetSentence(i));
            }

            DisplaySentence();
        }

        private void DisplaySentence()
        {
            if (_sentenceQueue.Count == 0)
            {
                EndDialogue();
                return;
            }

            _sentence = _sentenceQueue.Dequeue();

            Invoke(nameof(DisplaySentence), _dialogueDisplayTime);
        }

        private void EndDialogue()
        {
            InputManager.Instance.EnablePlayerInput();
            _dialogueCompleted = true;
            _sentence = null;
        }
    }
}
