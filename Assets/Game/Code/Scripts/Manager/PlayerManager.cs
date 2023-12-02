using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        private const string PLAYER_POOL_TAG = "Player";

        [Header("Player Lifes")]
        [SerializeField]
        private int _life;

        [Header("Player Spawn")]
        [SerializeField]
        private Transform _playerSpawnPoint;
        [SerializeField]
        private float _spawnTime;

        private CharacterAim _characterAim;
        private CharacterMovement _characterMovement;
        private CharacterHealth _characterHealth;
        private CharacterBasicShoot _characterBasicShoot;
        private CharacterUltimateShoot _characterUltimateShoot;
        private bool _isInitialSpawn;

        public GameObject Player { get; private set; }
        public CharacterAim CharacterAim => _characterAim;
        public CharacterMovement CharacterMovement => _characterMovement;
        public CharacterHealth CharacterHealth => _characterHealth;
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
            AudioManager.StopAllMusic();
            AudioManager.PlayMusic(MainMusic.battle_theme_01);
            _isInitialSpawn = true;
            SpawnPlayer();
        }

        public void SetupPlayer(BaseCharacter character)
        {
            _characterAim = character.GetComponent<CharacterAim>();
            _characterMovement = character.GetComponent<CharacterMovement>();
            _characterHealth = character.GetComponent<CharacterHealth>();
            _characterBasicShoot = character.GetComponent<CharacterBasicShoot>();
            _characterUltimateShoot = character.GetComponent <CharacterUltimateShoot>();
        }

        public void SpawnPlayer()
        {
            GameObject player = ObjectPooler.Instance.GetPooledObject(PLAYER_POOL_TAG, _playerSpawnPoint.position, Quaternion.identity);
            InputManager.Instance.EnablePlayerInput();
            CameraManager.Instance.SetupFollowCamera(player.transform);
            RespawnerManager.Instance.RespawnObjects();

            Player = player;

            if (!_isInitialSpawn)
            {
                _characterHealth.SetHealthPoints(_characterHealth.MaxHealthPoints);
                return;
            }

            _isInitialSpawn = false;
        }

        public void DecreaseLife()
        {
            _life--;

            if (_life <= 0)
            {
                Debug.Log($"[{nameof(PlayerManager)}] Game Over!");
            }
            else
            {
                Invoke(nameof(SpawnPlayer), _spawnTime);
            }
        }
    }
}
