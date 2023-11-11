using UnityEngine;

namespace ProjectTD
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(2, 4)]
        public string[] sentences;
        public bool canTriggerObjective;
    }
}
