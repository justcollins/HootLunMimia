using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;                      // Whether or not the player is currently sighted.
    public Vector3 personalLastSighting;            // Last place this enemy spotted the player.


    //private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
    private SphereCollider col;                     // Reference to the sphere collider trigger component.
    //private Animator anim;                          // Reference to the Animator.
    private GameObject player;                      // Reference to the player.
    //private Animator playerAnim;                    // Reference to the player's animator component.
    private int playerHealth;              // Reference to the player's health script.
    public Transform dumb;


    void Awake()
    {
        // Setting up the references.
        //nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        //anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        //previousSighting = personalLastSighting;
    }


    void OnTriggerStay(Collider other)
    {
        //Debug.Log("TRIGGERED");
        // If the player has entered the trigger sphere...
        if (other.gameObject == player)
        {
            //Debug.Log("TRIGGERED BY PLAYER");
            // By default the player is not in sight.
            playerInSight = false;

            // Create a vector from the enemy to the player and store the angle between it and forward.
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            //direction.y = 0;

            // If the angle between forward and where the player is, is less than half the angle of view...
            if (angle < fieldOfViewAngle * 0.5f)
            {
                //Debug.Log("GOOD ANGLE");
                RaycastHit hit;
                
                //transform.position = new Vector3(transform.position.x, 1, transform.position.z);//Fix for dumb
                //Debug.Log("POSITION:" + transform.position);
                //Debug.Log("PLYAR POS: " + player.transform.position);
                //Debug.Log("DIRECTION:" + direction);
                // ... and if a raycast towards the player hits something...
                if (Physics.Raycast(transform.position + transform.up, direction, out hit, col.radius))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.cyan, 60);
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject == player)
                    {
                        //Debug.Log("BIG WIN");
                        // ... the player is in sight.
                        playerInSight = true;
                        // Set the last global sighting is the players current position.
                        personalLastSighting = player.transform.position;
                    }
                }
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone...
        if (other.gameObject == player)
            // ... the player is not in sight.
            playerInSight = false;
    }
}