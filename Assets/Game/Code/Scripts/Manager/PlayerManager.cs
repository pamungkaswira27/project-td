using UnityEngine;

namespace ProjectTD
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [Header("Player Lifes")]
        [SerializeField]
        private int _life;

        [Header("Player Spawn")]
        [SerializeField]
        private Transform _playerSpawnPoint;
        [SerializeField]
        private float _spawnTime;

        private CharacterMovement _characterMovement;
        private CharacterBasicShoot _characterBasicShoot;
        private CharacterUltimateShoot _characterUltimateShoot;

        public CharacterMovement CharacterMovement => _characterMovement;
        public CharacterBasicShoot CharacterBasicShoot => _characterBasicShoot;
        public CharacterUltimateShoot CharacterUltimateShoot => _characterUltimateShoot;
        public int Life => _life;

        public Transform PlayerSpawnPoint
        {
            set => _playerSpawnPoint = value;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnPlayer();
        }

        public void SetupPlayer(BaseCharacter character)
        {
            _characterMovement = character.GetComponent<CharacterMovement>();
            _characterBasicShoot = character.GetComponent<CharacterBasicShoot>();
            _characterUltimateShoot = character.GetComponent <CharacterUltimateShoot>();
        }

        public void SpawnPlayer()
        {
            GameObject player = ObjectPooler.Instance.GetPooledObject("Player", _playerSpawnPoint.position, Quaternion.identity);
            CameraManager.Instance.SetupFollowCamera(player.transform);
        }

        public void DecreaseLife()
        {
            _life--;

            if (_life <= 0)
            {
                Debug.Log($"[{GetType().Name}] Game Over!");
            }
            else
            {
                Invoke(nameof(SpawnPlayer), _spawnTime);
            }
        }
    }
}
