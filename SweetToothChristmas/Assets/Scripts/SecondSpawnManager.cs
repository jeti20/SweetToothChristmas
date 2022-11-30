using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSpawnManager : MonoBehaviour
{
    public GameObject _enemyPrefab;
    private float _spawnRange = 9f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        //spawning enemys in random position
        float _spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float _spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        Vector3 _randomPos = new Vector3(_spawnPosX, 0, _spawnPosZ);
        Instantiate(_enemyPrefab, _randomPos, _enemyPrefab.transform.rotation);
    }

}
