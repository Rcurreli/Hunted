using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimento : MonoBehaviour
{

    public float velocita = 1.0f;
    public GameObject Omino;
    private Rigidbody rb;
    private bool controllo;
    private bool move;
    private bool riposiziona = false;
    private Vector3 moveDir;
    private Vector3 raysDirBlu;
    private int raysAsse; 
    private Vector3 rayPosRossoR;
    private Vector3 rayPosRossoL;
    private RaycastHit rayHitRossoR;
    private RaycastHit rayHitRossoL;
    private bool rayFindRossoR;
    private bool rayFindRossoL;
    private Vector3 rayPosBluR;
    private Vector3 rayPosBluL;
    private RaycastHit rayHitBluR;
    private RaycastHit rayHitBluL;
    private bool rayFindBluR;
    private bool rayFindBluL;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {    
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if(Omino.transform.rotation == Quaternion.LookRotation(transform.forward) 
            || Omino.transform.rotation == Quaternion.LookRotation(- transform.forward)){
                moveDir = transform.forward;
                move = true;
            }
            else{
                raysAsse = 0;
                raysDirBlu = transform.forward;
                controllo = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if(Omino.transform.rotation == Quaternion.LookRotation(- transform.forward) 
            || Omino.transform.rotation == Quaternion.LookRotation(transform.forward)){
                moveDir = - transform.forward;
                move = true;
            }    
            else{
                raysAsse = 0;
                raysDirBlu = - transform.forward;
                controllo = true;
            }    
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if(Omino.transform.rotation == Quaternion.LookRotation(- transform.right) 
            || Omino.transform.rotation == Quaternion.LookRotation(transform.right)){
                moveDir = - transform.right;
                move = true;
            }  
            else{
                raysAsse = 1;
                raysDirBlu = - transform.right;
                controllo = true;
            }      
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if(Omino.transform.rotation == Quaternion.LookRotation(transform.right) 
            || Omino.transform.rotation == Quaternion.LookRotation(- transform.right)){
                moveDir = transform.right;
                move = true;
            }
            else{
                raysAsse = 1;
                raysDirBlu = transform.right;
                controllo = true;
            }  
        }
        else move = false;

        //Raycast svolta in FixedUpdate

        //Movimento in FixedUpdate

        //Raycast davanti in FixedUpdate

    }

    void FixedUpdate() {
        if(controllo){
            if(RaysTurn()){
                moveDir = raysDirBlu;
                riposiziona = true;
                move = true;
                controllo = false;
            }
        }

        if(move){
            Movimento(moveDir);
            move = false;
        }

        if(RaysForward()){
            move = false;
            rb.velocity = new Vector3(0,0,0);
            if(Omino.transform.rotation == Quaternion.LookRotation(transform.forward)
            || Omino.transform.rotation == Quaternion.LookRotation(- transform.forward)){
                HitReset(1); 
            }
            else if(Omino.transform.rotation == Quaternion.LookRotation(transform.right)
            || Omino.transform.rotation == Quaternion.LookRotation(- transform.right)){
                HitReset(0);
            }
        }
    }

    bool RaysTurn(){
        rayPosBluR = transform.position;
        rayPosBluR.y += 0.5f;
        rayPosBluL = transform.position;
        rayPosBluL.y += 0.5f;
        if(raysAsse == 0){
            rayPosBluR.x += 0.4f;
            rayPosBluL.x -= 0.4f;
        }
        else if(raysAsse == 1){
            rayPosBluR.z += 0.4f;
            rayPosBluL.z -= 0.4f;
        }
        rayFindBluR = Physics.Raycast(rayPosBluR, raysDirBlu, out rayHitBluR, 0.6f);
        rayFindBluL = Physics.Raycast(rayPosBluL, raysDirBlu, out rayHitBluL, 0.6f);
        
        Debug.DrawRay(rayPosBluR, raysDirBlu, Color.black);
        Debug.DrawRay(rayPosBluL, raysDirBlu, Color.blue);

        if ((!rayFindBluR || rayHitBluR.transform.tag != "Muro")
        &&(!rayFindBluL || rayHitBluL.transform.tag != "Muro")) {
            return true;
        } 
        return false;
    }

    void Movimento(Vector3 dir){
        Omino.transform.rotation = Quaternion.LookRotation(dir);
        if(riposiziona){
            HitReset(0);
            HitReset(1);
            riposiziona = false;
        }
        rb.velocity = dir * velocita;  
    }

    bool RaysForward(){
        rayPosRossoR = transform.position;
        rayPosRossoR.y += 0.5f;
        rayPosRossoL = transform.position;
        rayPosRossoL.y += 0.5f;
        if(Omino.transform.rotation == Quaternion.LookRotation(- transform.forward) 
        || Omino.transform.rotation == Quaternion.LookRotation(transform.forward)){
            rayPosRossoR.x += 0.47f;
            rayPosRossoL.x -= 0.47f;
        }
        else if(Omino.transform.rotation == Quaternion.LookRotation(- transform.right) 
        || Omino.transform.rotation == Quaternion.LookRotation(transform.right)){
            rayPosRossoR.z += 0.47f;
            rayPosRossoL.z -= 0.47f;
        }
        rayFindRossoR = Physics.Raycast(rayPosRossoR, Omino.transform.forward, out rayHitRossoR, 0.49f);
        rayFindRossoL = Physics.Raycast(rayPosRossoL, Omino.transform.forward, out rayHitRossoL, 0.49f);

        Debug.DrawRay(rayPosRossoR, Omino.transform.forward, Color.red);
        Debug.DrawRay(rayPosRossoL, Omino.transform.forward, Color.red);

        if(rayFindRossoR){
            if(rayHitRossoR.transform.tag == "Muro"){
                return true;
            } 
        }
        else if(rayFindRossoL){
            if(rayHitRossoL.transform.tag == "Muro"){
                return true;
            } 
        } 
        return false;           
    }

    void HitReset(int asse){
        if(asse == 0){
            int divisione = (int)(transform.position.x / 0.5f);
            if((transform.position.x - (divisione * 0.5f))> 0.3f) divisione +=1;
            else if((transform.position.x - (divisione * 0.5f))< -0.3f) divisione -=1;
            transform.position = new Vector3(divisione * 0.5f,transform.position.y,transform.position.z);
        }
        else if(asse == 1){
            int divisione = (int)(transform.position.z / 0.5f);
            if((transform.position.z - (divisione * 0.5f))> 0.3f) divisione +=1;
            else if((transform.position.z - (divisione * 0.5f))< -0.3f) divisione -=1;
            transform.position = new Vector3(transform.position.x,transform.position.y,divisione * 0.5f);  
        }
    }
}
