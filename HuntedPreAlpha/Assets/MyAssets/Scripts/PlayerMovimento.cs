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
    private int asse; 
    private Vector3 miaPosizione1;
    private Vector3 miaPosizione2;
    private RaycastHit chivedo1;
    private RaycastHit chivedo2;
    private bool vedoqualcosa1;
    private bool vedoqualcosa2;
    private Vector3 miaPosizione3;
    private Vector3 miaPosizione4;
    private RaycastHit chivedo3;
    private RaycastHit chivedo4;
    private bool vedoqualcosa3;
    private bool vedoqualcosa4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ominoDir = Omino.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {    
        ominoDir = Omino.transform.rotation;
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if(ominoDir == Quaternion.LookRotation(transform.forward)){
                Movimento(transform.forward);
            }
            else if(ominoDir == Quaternion.LookRotation(- transform.forward)){
                Movimento(transform.forward);
                raggioDir = transform.forward;
            }
            else{
                asse = 0;
                raggioDir = transform.forward;
                controllo = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if(ominoDir == Quaternion.LookRotation(- transform.forward)){
                Movimento(- transform.forward);
            }    
            else if(ominoDir == Quaternion.LookRotation(transform.forward)){
                Movimento(- transform.forward);
                raggioDir = - transform.forward;
            }
            else{
                asse = 0;
                raggioDir = - transform.forward;
                controllo = true;
            }    
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if(ominoDir == Quaternion.LookRotation(- transform.right)){
                Movimento(- transform.right);
            }  
            else if(ominoDir == Quaternion.LookRotation(transform.right)){
                Movimento(- transform.right);
                raggioDir = - transform.right;
            }
            else{
                asse = 1;
                raggioDir = - transform.right;
                controllo = true;
            }      
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if(ominoDir == Quaternion.LookRotation(transform.right)){
                Movimento(transform.right);
            }
            else if(ominoDir == Quaternion.LookRotation(- transform.right)){
                Movimento(transform.right);
                raggioDir = transform.right;
            }  
            else{
                asse = 1;
                raggioDir = transform.right;
                controllo = true;
            }  
        }

        //Raycast svolta
        if(controllo){
            if(raggioDir.x != 0 || raggioDir.z != 0){
                miaPosizione3 = transform.position;
                miaPosizione3.y += 0.5f;
                miaPosizione4 = transform.position;
                miaPosizione4.y += 0.5f;
                if(asse == 0){
                    miaPosizione3.x += 0.49f;
                    miaPosizione4.x -= 0.49f;
                }
                else if(asse == 1){
                    miaPosizione3.z += 0.49f;
                    miaPosizione4.z -= 0.49f;
                }
                vedoqualcosa3 = Physics.Raycast(miaPosizione3, raggioDir, out chivedo3, 0.6f);
                vedoqualcosa4 = Physics.Raycast(miaPosizione4, raggioDir, out chivedo4, 0.6f);

                if ((!vedoqualcosa3 || chivedo3.transform.tag != "Muro")&&(!vedoqualcosa4 || chivedo4.transform.tag != "Muro")) {
                    Movimento(raggioDir);
                    controllo = false;
                } 
                Debug.DrawRay(miaPosizione3, raggioDir, Color.blue);
                Debug.DrawRay(miaPosizione4, raggioDir, Color.blue);
            }
        }

        //Raycast davanti
        miaPosizione1 = transform.position;
        miaPosizione1.y += 0.5f;
        miaPosizione2 = transform.position;
        miaPosizione2.y += 0.5f;
        ominoDir = Omino.transform.rotation;
        if(ominoDir == Quaternion.LookRotation(- transform.forward) || ominoDir == Quaternion.LookRotation(transform.forward)){
            miaPosizione1.x += 0.49f;
            miaPosizione2.x -= 0.49f;
        }
        else if(ominoDir == Quaternion.LookRotation(- transform.right) || ominoDir == Quaternion.LookRotation(transform.right)){
            miaPosizione1.z += 0.49f;
            miaPosizione2.z -= 0.49f;
        }
        vedoqualcosa1 = Physics.Raycast(miaPosizione1, Omino.transform.forward, out chivedo1, 0.49f);
        vedoqualcosa2 = Physics.Raycast(miaPosizione2, Omino.transform.forward, out chivedo2, 0.49f);
        if(vedoqualcosa1){
            if(chivedo1.transform.tag == "Muro"){
                rb.velocity*=0;
                if(ominoDir == Quaternion.LookRotation(- transform.forward) || ominoDir == Quaternion.LookRotation(transform.forward)){
                    int divisione = (int)(transform.position.z / 0.5f);
                    transform.position = new Vector3(transform.position.x,transform.position.y,divisione * 0.5f);
                }
            } 
        }
        if(vedoqualcosa2){
            if(chivedo2.transform.tag == "Muro"){
                rb.velocity*=0;
                if(ominoDir == Quaternion.LookRotation(- transform.right) || ominoDir == Quaternion.LookRotation(transform.right)){
                    int divisione = (int)(transform.position.x / 0.5f);
                    transform.position = new Vector3(divisione * 0.5f,transform.position.y,transform.position.z);
                }
            } 
        }
        Debug.DrawRay(miaPosizione1, Omino.transform.forward, Color.red);
        Debug.DrawRay(miaPosizione2, Omino.transform.forward, Color.red);
    }

    private void Movimento(Vector3 dir){
        Omino.transform.rotation = Quaternion.LookRotation(dir);
        rb.velocity = dir * velocita;  
    }

}
