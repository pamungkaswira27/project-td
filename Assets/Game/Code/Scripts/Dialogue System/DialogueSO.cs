using UnityEngine;

namespace ProjectTD
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "ProjectTD/Create Dialogue")]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField, TextArea(2, 4)]
        private string[] _sentences;

        public string GetSentence(int index)
        {
            return _sentences[index];
        }

        public int GetSentenceLength()
        {
            return _sentences.Length;
        }
    }
}
