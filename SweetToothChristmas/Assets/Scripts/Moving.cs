using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 20f;
    public float verticalInput;
    public float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        //identyfikacja obiektu i dostanie siê do w³aœciwoœci komponentu.
        GameObject Cube = GameObject.Find("Player");
        Rigidbody Body = Cube.GetComponent<Rigidbody>();
        Body.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
    }
}
