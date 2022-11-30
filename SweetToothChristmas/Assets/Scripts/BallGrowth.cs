using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGrowth : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    private Vector3 scaleChange, positionChange;

    private void Awake()
    {
        scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void Start()
    {
        ;
    }

    private void Update()
    {
        if (transform.localScale.x < 20)
        {
            transform.localScale += scaleChange * Time.deltaTime;
        }
    }
}
