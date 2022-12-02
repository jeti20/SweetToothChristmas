using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{//make enemy follow "Player" with constant speed
    public float _speed = 3f;
    private Rigidbody _enemyRb;
    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //_enemyRb.AddForce((_player.transform.position - transform.position).normalized * _speed); //czym dalej jest przecwinik do gracza tym wiêksza róznica wiêc i spped jest wiêkszy, dlatego uzywamy normalized które mówi nam ¿e bêdzie mia³o taki sam speed
        Vector3 _lookDirection = (_player.transform.position - transform.position).normalized; //to samo co wy¿ej
        _enemyRb.AddForce(_lookDirection * _speed);

        //niszczy enemy jeœli spadnie 
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
