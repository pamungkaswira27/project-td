using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCrates : SpawnManager
{
    [Header("Spawn Position")]
    [SerializeField]
    private int _maxPositionX;
    [SerializeField]
    private int _maxPositionZ;

    private GameObject _cratesPrefab;

    private void Start()
    {
        SpawningCrates();
    }

    protected override void SpawningCrates()
    {
        base.SpawningCrates();

        Vector3 post = new Vector3(Random.Range(0, _maxPositionX), 0, Random.Range(0, _maxPositionZ));

        _cratesPrefab = _objectPooler.GetPooledObject("Crates", post, Quaternion.identity);
    }
}
