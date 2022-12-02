using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{

    [Header("Player Setting")]
    public float turnSpeed = 10f;
    public float runSpeed = 3f;
    public bool stopMoverment = false;
    public ParticleSystem particalSystem;

    public bool moving { get; set; }

    float m_Horizontal, m_Vertical;
    private Vector3 m_MoveVector;
    private Rigidbody m_Rigidbody;
    public AudioSource deadSound;
    public AudioSource collectionSound;

    [HideInInspector]
    public Animator m_Animator;
    private Quaternion m_Rotation = Quaternion.identity;
    private Transform camTrans;
    private Vector3 camForward;
    public bool _hasPowerup = false;
    public bool Alive = true;
    public GameObject mesh;


    private Vector3 offset;
    //public GameObject _powerUpIndicator; //obręcz po zebraniu cukierka
    private float _powerUpStregnth = 15; //siła odrzucenia wrgoga po cukierki

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        camTrans = Camera.main.transform;
    }


    void FixedUpdate()
    {

        //input 
        m_Horizontal = Input.GetAxis(Const.Horizontal);
        m_Vertical = Input.GetAxis(Const.Vetical);

        // move vector 
        if (camTrans != null)
        {
            camForward = Vector3.Scale(camTrans.forward, new Vector3(1, 0, 1).normalized);
            m_MoveVector = m_Vertical * camForward + m_Horizontal * camTrans.right;
            m_MoveVector.Normalize();
        }
        //animation    
        bool has_H_Input = !Mathf.Approximately(m_Horizontal, 0);
        bool has_V_Input = !Mathf.Approximately(m_Vertical, 0);

        if (!stopMoverment) moving = has_H_Input || has_V_Input;
        else moving = false;

        float inputSpeed = Mathf.Clamp01(Mathf.Abs(m_Horizontal) + Mathf.Abs(m_Vertical));

        m_Animator.SetBool(Const.Moving, moving);
        m_Animator.SetFloat(Const.Speed, inputSpeed);

        //move and rotate
        if (moving)
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_MoveVector, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredForward), turnSpeed);
            m_Rigidbody.MoveRotation(m_Rotation);
            m_Rigidbody.MovePosition(m_Rigidbody.position + inputSpeed * m_MoveVector * runSpeed * Time.deltaTime);
        }

        //po zebraniu PowerUp, poruszanie się obwódki
        //_powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    //zbieranie PowerUp
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            _hasPowerup = false;
            collectionSound.Play();
            Destroy(other.gameObject);
            _hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
            //_powerUpIndicator.gameObject.SetActive(true);
        }
    }

    //czas trwania powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(10
            );
        _hasPowerup = false;
        //_powerUpIndicator.gameObject.SetActive(false);

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
        }
        else
        {
            StartCoroutine(BackToMenu());
        }

    }

    IEnumerator BackToMenu()
    {
        Alive = false;
        mesh.SetActive(false);
        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        particalSystem.Play();
        deadSound.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
        Debug.Log("XD");
    }
}




