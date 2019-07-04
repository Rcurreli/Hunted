using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimento : MonoBehaviour
{

    public float velocita = 1.0f;
    public GameObject Omino;
    private Rigidbody rb;
    private bool controllo;
    private Quaternion ominoDir;
    private Vector3 raggioDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ominoDir = Omino.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {    
        ominoDir = Omino.transform.rotation;
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if(ominoDir == Quaternion.LookRotation(transform.forward)){
                Movimento(transform.forward);
            }
            else if(ominoDir == Quaternion.LookRotation(- transform.forward)){
                Movimento(transform.forward);
            }
            else{
                raggioDir = transform.forward;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if(ominoDir == Quaternion.LookRotation(- transform.forward)){
                Movimento(- transform.forward);
            }    
            else if(ominoDir == Quaternion.LookRotation(transform.forward)){
                Movimento(- transform.forward);
            }
            else{
                raggioDir = - transform.forward;
            }    
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if(ominoDir == Quaternion.LookRotation(- transform.right)){
                Movimento(- transform.right);
            }  
            else if(ominoDir == Quaternion.LookRotation(transform.right)){
                Movimento(- transform.right);
            }
            else{
                raggioDir = - transform.right;
            }      
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if(ominoDir == Quaternion.LookRotation(transform.right)){
                Movimento(transform.right);
            }
            else if(ominoDir == Quaternion.LookRotation(- transform.right)){
                Movimento(transform.right);
            }  
            else{
                raggioDir = transform.right;
            }  
        }

        Vector3 miaPosizione = transform.position;
		miaPosizione.y += 0.5f;

		RaycastHit chivedo;
		bool vedoqualcosa = Physics.Raycast(miaPosizione, raggioDir, out chivedo, 10f);

		if (vedoqualcosa && chivedo.transform.tag == "Muro") {
            
		} 
        else {
			GetComponent<Animator>().SetBool("tivedo", false);
		}

    }

    private void Movimento(Vector3 dir){
        Omino.transform.rotation = Quaternion.LookRotation(dir);
        rb.velocity = dir * velocita;  
    }

}
