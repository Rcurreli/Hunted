using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Nemico : MonoBehaviour
{
    public GameObject bersaglio;
    private NavMeshAgent nemicoAgent;

    // Start is called before the first frame update
    void Start()
    {
        nemicoAgent = GetComponent<NavMeshAgent>();
        StartCoroutine("Pathfinding");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Pathfinding(){
        while(true){
            nemicoAgent.destination = bersaglio.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}
