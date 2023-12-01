using UnityEngine;

namespace ProjectTD
{
    public class ItemSO : ScriptableObject
    {
        [SerializeField]
        protected string _id;

        public virtual void Use() { }
    }
}
