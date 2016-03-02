using UnityEngine;
using System.Collections;

public class RoomDetection : MonoBehaviour {

    public EnemyPathing enemy;
    public GameObject plyr;
    
	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<EnemyPathing>();
        plyr = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(enemy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        enemy.MyOnTriggerEnter(other);
    }
}
