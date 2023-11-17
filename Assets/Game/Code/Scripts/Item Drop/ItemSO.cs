using UnityEngine;

namespace ProjectTD
{
    public class ItemSO : ScriptableObject
    {
        [SerializeField]
        protected string _name;

        public virtual void Use() { }
    }
}
