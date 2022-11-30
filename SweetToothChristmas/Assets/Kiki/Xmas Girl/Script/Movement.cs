using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Movement : MonoBehaviour
    {
    public float _moveSpeed;
    public Rigidbody _rb;
    private Vector3 _moveInput;

    bool _hasPowerup = false;
    public GameObject _powerUpIndicator; //obręcz po zebraniu cukierka
    private float _powerUpStregnth = 15; //siła odrzucenia wrgoga po cukierki



    private void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        _moveInput.Normalize();

        _rb.velocity = _moveInput * _moveSpeed;
    }



    //zbieranie PowerUp
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

        //odrzucanie wroga po zebraniu cukierka 
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

    


