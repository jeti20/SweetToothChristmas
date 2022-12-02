using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnManager : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public GameObject _powerUpPrefab;
    public GameObject scoreTextInGame;
    private float _spawnRange = 9f;
    public int _enemyCount;
    public  int _waveNumber = 1;

    [SerializeField] PlayerController _playercontroller;

    void Start()
    {
        SpawnEnemyWave(_waveNumber);
        
        //spawnujemy powerup'y w losowych miejscach
        Instantiate(_powerUpPrefab, GenerateSpawnPositionEnemy() + new Vector3(0, 1, 0), _powerUpPrefab.transform.rotation);
        
    }

    public void Update()
    {
        //si�gamy do skryptu Enemy aby sprawdzi� ilo�c wrog�w na podstawie przypisanych do nich skryptu Enemy, albo tagu, jesli zepchniemy i zniknie jeden z trzech to wy�wietli 2 (real time)
        _enemyCount = FindObjectsOfType<Enemy>().Length;
       
        //sprawdza, czy wszyscy wrogowie zostali zepchni�ci (ich liczba == 0) i je�li tak to wykonuje inkementracje do wavenumber kt�a decyduje o tym ile zrespi si� nowych przeciwnik�w w fali
        if (_enemyCount == 0 && _playercontroller.Alive == true)
        {
            _waveNumber++;
            ChceckHighScore();
            SpawnEnemyWave(_waveNumber);     
            //za ka�d� fal� spawnuje sie jeden powerup
            Instantiate(_powerUpPrefab, GenerateSpawnPositionEnemy() + new Vector3(0, 1, 0), _powerUpPrefab.transform.rotation);
        }       
        UpdatingHighScore();
    }

    //sprawdzanie najwi�kszego wyniku
    public void ChceckHighScore()
    {
        if (_waveNumber > PlayerPrefs.GetInt("HighScore"));
        {
            PlayerPrefs.SetInt("HighScore", _waveNumber);
        }
    }

    //wy�wietlanie wyniku
    void UpdatingHighScore()
    {
        scoreTextInGame.GetComponent<TextMeshProUGUI>().text = "Wave: " + _waveNumber;
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
