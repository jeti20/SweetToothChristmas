using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerrRb;
    private GameObject _focalPoint; //chcemy ¿eby si³a bdzia³a³a na gracza tak jak kamera jest obrócona
    public GameObject _powerUpIndicator;
    public float _speed = 5f;
    public bool _hasPowerup = false;
    private float _powerUpStregnth = 15;

    // Start is called before the first frame update
    void Start()
    {
        _playerrRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float _forwardInput = Input.GetAxis("Vertical");
        _playerrRb.AddForce(_focalPoint.transform.forward * _forwardInput * _speed);

        _powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    //usefull beteen coliders
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            _hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
            _powerUpIndicator.gameObject.SetActive(true);
        }
    }

    //czas trwania powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        _hasPowerup = false;
        _powerUpIndicator.gameObject.SetActive(false);

    }

    //better for physics, odrzucanie enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _hasPowerup)
        {
            //po podniesieniu powerup i zderzeniu z enemy  odrzucamy go dalej
            Rigidbody _enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 _awayFromPlayer = collision.gameObject.transform.position - transform.position;
            _enemyRigidbody.AddForce(_awayFromPlayer * _powerUpStregnth, ForceMode.Impulse);

            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + _hasPowerup);
        }
    }
}
