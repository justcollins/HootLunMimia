using UnityEngine;
using System.Collections;

public class EnemyPathing : MonoBehaviour {

    NavMeshAgent agent;
    private GameObject plyr;

    void Start()
    {
        plyr = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        agent.SetDestination(plyr.transform.position);
    }}


/*
    
*/