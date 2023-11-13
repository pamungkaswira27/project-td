using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [SerializeField]
        private float _nextSentenceTime;

        private Queue<string> _sentenceQueue;
        private Dialogue _currentDialogue;

        private string _sentence;
        public string Sentence => _sentence;

        private void Awake()
        {
            Instance = this;
            _sentenceQueue = new Queue<string>();
        }

        private void Start()
        {
            
        }

        public void StartDialogue(Dialogue dialogue)
        {
            _currentDialogue = dialogue;
            _sentenceQueue.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                _sentenceQueue.Enqueue(sentence);
            }

            DisplaySentence();
        }

        public void ClearSentence()
        {
            _sentence = null;
        }

        private void DisplaySentence()
        {
            if (_sentenceQueue.Count == 0)
            {
                EndDialogue();
                return;
            }

            _sentence = _sentenceQueue.Dequeue();

            // Display next sentence
            Invoke(nameof(DisplaySentence), _nextSentenceTime);
        }

        private void EndDialogue()
        {
            if (_currentDialogue.canTriggerObjective)
            {
                ObjectiveManager.Instance.SetActiveObjective();
            }

            if (ObjectiveManager.Instance.ActiveObjective == null)
            {
                _sentence = string.Empty;
                return;
            }

            _sentence = ObjectiveManager.Instance.ActiveObjective.Description;
            _currentDialogue = null;
            InputManager.Instance.EnablePlayerInput();

            Debug.Log($"[{nameof(DialogueManager)}]: Dialogue end");
        }
    }
}
