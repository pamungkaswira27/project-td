using UnityEngine;

namespace ProjectTD
{
    public class KillObjective : BaseObjective
    {
        [SerializeField]
        private CharacterHealth[] _targets;

        private int _targetCount;

        public override void Initialize()
        {
            _targetCount = _targets.Length;

            for (int i = 0; i < _targets.Length; i++)
            {
                if (_targets[i] == null)
                {
                    return;
                }

                _targets[i].OnCharacterDead += OnTargetKilled;
            }
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override bool IsObjectiveCompleted()
        {
            return (_targetCount <= 0);
        }

        private void OnTargetKilled()
        {
            _targetCount--;
        }
    }
}
