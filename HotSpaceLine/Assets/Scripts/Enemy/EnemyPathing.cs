using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathing : MonoBehaviour {

    NavMeshAgent agent;
    private GameObject plyr;
    EnemySight enemySightSeal;
    public List<Transform> patrolLocs = new List<Transform>();
    private int iterator = 0;
    private int iterMAX = 0;
    public GameObject startingRoom;

    void Start()
    {
        plyr = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        enemySightSeal = this.GetComponent<EnemySight>();
        readInPatrol(startingRoom);
    }

    void Update()
    {
        if (enemySightSeal.playerInSight == true)
            agent.SetDestination(plyr.transform.position);
        else
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        if (patrolLocs != null)
                        {
                            iterator = (iterator + 1) % iterMAX;
                            //Debug.Log("ITERATOR: " + iterator);
                            //Debug.Log("MAX: " + iterMAX);
                            agent.SetDestination(patrolLocs[iterator].position);
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Room")
        {
            Debug.Log("CLEAR IT OUT BOIS");
            patrolLocs.Clear();
            iterMAX = 0;
            if (patrolLocs.Count == 0)
            {
                iterator = 0;
                readInPatrol(other.gameObject);
            }
        }
    }

    void readInPatrol(GameObject room)
    {
        foreach (Transform child in room.transform)
        {
            //Debug.Log(child);
            patrolLocs.Add(child);
            iterMAX++;
        }
    }
}