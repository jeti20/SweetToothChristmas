using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public GameObject _powerUpPrefab;
    private float _spawnRange = 9f;
    public int _enemyCount;
    public int _waveNumber = 1;

    void Start()
    {
        SpawnEnemyWave(_waveNumber);

        //spawnujemy powerup'y w losowych miejscach
        Instantiate(_powerUpPrefab, GenerateSpawnPositionEnemy() + new Vector3(0, 1, 0), _powerUpPrefab.transform.rotation);
    }

    void Update()
    {
        //si�gamy do skryptu Enemy aby sprawdzi� ilo�c wrog�w na podstawie przypisanych do nich skryptu Enemy, albo tagu, jesli zepchniemy i zniknie jeden z trzech to wy�wietli 2 (real time)
        _enemyCount = FindObjectsOfType<Enemy>().Length;
        
        //sprawdza, czy wszyscy wrogowie zostali zepchni�ci (ich liczba == 0) i je�li tak to wykonuje inkementracje do wavenumber kt�a decyduje o tym ile zrespi si� nowych przeciwnik�w w fali
        if (_enemyCount == 0)
        {
            _waveNumber++;
            SpawnEnemyWave(_waveNumber);

            //za ka�d� fal� spawnuje sie jeden powerup
            Instantiate(_powerUpPrefab, GenerateSpawnPositionEnemy() + new Vector3(0, 1, 0), _powerUpPrefab.transform.rotation);
        }
    }

    //logika spawnowania fal przeciwnik�w. 
    void SpawnEnemyWave(int _enemysToSpawn)
    {
        for (int i = 0; i < _enemysToSpawn; i++)
        {
            Instantiate(_enemyPrefab, GenerateSpawnPositionEnemy(), _enemyPrefab.transform.rotation);
        }
    }
    //losuje pozycje w kt�ej b�dzie zespawnowany wr�g. return poniewa� zwraca nam pozycj� kt�r� chcemy
    private Vector3 GenerateSpawnPositionEnemy()
    {
        //spawning enemys in random position
        float _spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float _spawnPosZ = Random.Range(-_spawnRange, _spawnRange);

        Vector3 _randomPos = new Vector3(_spawnPosX, 0, _spawnPosZ);

        return _randomPos;
    }



}
