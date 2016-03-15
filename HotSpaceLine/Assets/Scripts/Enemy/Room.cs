using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    public List<EnemyPathing> Enemies = new List<EnemyPathing>();
    private GameObject Player;
    private bool playerPresent = false;
    private bool playerAdjacent = false;
    public Room[] adjacentRooms;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
    //If CanShoot && MouseClick
	void Update () {
        for (int i = 0; i < adjacentRooms.Length; i++)
        {
            if (adjacentRooms[i].GetPlyrPres())
            {
                playerAdjacent = true;
            }
            else
            {
                playerAdjacent = false;
            }
        }
	    if(playerPresent == true || playerAdjacent == true)
        {
            if(Player.GetComponent<WeaponController>().GetCurrentGun().CanShoot() )
            {
                if( Input.GetButtonDown("Shoot") )
                {
                    for(int i = 0; i < Enemies.Count; i++)
                    {
                        Enemies[i].SetAgent(Player.transform);
                    }
                }
            }
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject;
            playerPresent = true;
        }
        if (other.tag == "RoomDetect")//If other is an enemy
        {
            if( !Enemies.Contains(other.GetComponentInParent<EnemyPathing>()));// And if it is not in the list
            Enemies.Add(other.GetComponentInParent<EnemyPathing>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject;
            playerPresent = false;
        }
        if (other.tag == "RoomDetect");//If other is an enemy
        {
            for(int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i] == other.GetComponentInParent<EnemyPathing>())
                {
                    Enemies.Remove(Enemies[i]);
                }
            }
        }
    }
    bool GetPlyrPres()
    {
        return playerPresent;
    }
}
