using UnityEngine;

namespace ProjectTD
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        private CharacterMovement _characterMovement;
        private CharacterBasicShoot _characterBasicShoot;
        private CharacterUltimateShoot _characterUltimateShoot;

        public CharacterMovement CharacterMovement
        {
            get
            {
                return _characterMovement;
            }
        }

        public CharacterBasicShoot CharacterBasicShoot
        {
            get
            {
                return _characterBasicShoot;
            }
        }

        public CharacterUltimateShoot CharacterUltimateShoot
        {
            get
            {
                return _characterUltimateShoot;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        public void SetupPlayer(BaseCharacter character)
        {
            _characterMovement = character.GetComponent<CharacterMovement>();
            _characterBasicShoot = character.GetComponent<CharacterBasicShoot>();
            _characterUltimateShoot = character.GetComponent <CharacterUltimateShoot>();
        }
    }
}
