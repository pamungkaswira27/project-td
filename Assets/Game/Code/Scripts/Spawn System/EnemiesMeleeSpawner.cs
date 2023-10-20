using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StinkySteak.SimulationTimer;

public class EnemiesMeleeSpawner : SpawnManager
{
    private GameObject _enemiesMelee;
    private Vector3 _spotSpwan;
    private SimulationTimer _cooldownSpawn;

    [Header("Number of Spawn")]
    [SerializeField]
    private int _numberOfSpawn;
    [SerializeField]
    private int _maxNumberOfSpawn;
    [Header("Spawn Duration")]
    [SerializeField]
    private float _durationSpawn;
    [Header("Spawn Position")]
    [SerializeField]
    private float _maxPostX;
    [SerializeField]
    private float _maxPostZ;

    private void Start()
    {
        _cooldownSpawn = SimulationTimer.CreateFromSeconds(_durationSpawn);
    }

    private void Update()
    {
        if (_cooldownSpawn.IsExpired())
        {
            StartCoroutine(SpawningTime());
            _cooldownSpawn = SimulationTimer.None;
            _cooldownSpawn = SimulationTimer.CreateFromSeconds(_durationSpawn);
            return;
        }
        StopCoroutine(SpawningTime());

    }

    protected override void SpawningEnemies()
    {
        base.SpawningEnemies();
        Debug.Log("Spawning Enemies");
        _spotSpwan = new Vector3(Random.Range(transform.position.x, _maxPostX), 0, Random.Range(transform.position.z, _maxPostZ));
        _facingEnemiesSpawn.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        _enemiesMelee = _objectPooler.GetPooledObject("EnemiesMelee", _spotSpwan, _facingEnemiesSpawn.rotation);
        Debug.Log("Spawning Enemies");
    }

    protected override IEnumerator SpawningTime()
    {
        while (_numberOfSpawn < _maxNumberOfSpawn)
        {
            SpawningEnemies();
            yield return new WaitForSeconds(0.2f);
            _numberOfSpawn++;
        }
        Debug.Log("Spawning Enemies Complete");
    }

}
