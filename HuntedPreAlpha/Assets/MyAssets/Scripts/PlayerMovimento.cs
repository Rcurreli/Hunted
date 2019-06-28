using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimento : MonoBehaviour
{
    public float vel = 1.0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {     
        transform.forward* Input.GetAxis("Verticale") * vel * Time.deltaTime);
        transform.forward* Input.GetAxis("Orizzontale") * vel * Time.deltaTime);

        float velocita = vel + (transform.rotation.ToEulerAngles().x * 10);

        rb.velocity = transform.forward * velocita;
    }
}
