using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private spawnManager spawnManager;
    public GameObject scoreTextInGame; //text w UI

    private void Start()
    {
        //ChceckHighScore();
    }

    private void Update()
    {
        scoreTextInGame.GetComponent<TextMeshProUGUI>().text = "Wave: " + PlayerPrefs.GetInt("HighScore", 0);
        
    }

  /*  void ChceckHighScore()
    {
        if (spawnManager._waveNumber > PlayerPrefs.GetInt("HighScore", 0));
        {
            PlayerPrefs.SetInt("HighScore", spawnManager._waveNumber);
        }
    }*/
}
