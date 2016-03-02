using UnityEngine;
using System.Collections;

public class RoomDetection : MonoBehaviour {

    public EnemyPathing enemy;

	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<EnemyPathing>();
        Debug.Log(enemy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OTHERDETECTEDROOMDETECT");
        enemy.MyOnTriggerEnter(other);
    }
}
