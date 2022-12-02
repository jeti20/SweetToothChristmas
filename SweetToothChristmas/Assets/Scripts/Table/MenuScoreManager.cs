using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScoreManager : MonoBehaviour
{
    
    public TMP_Text scoreTextInGame; //text w UI


    private void Start()
    {
        scoreTextInGame.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
