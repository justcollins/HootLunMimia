using UnityEngine;
using System.Collections;

public class LazerBox : MonoBehaviour {

    private PlayerHealth player;

    private void Start() {
        player = GameObject.FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider playerCol) {
        if (playerCol.gameObject.tag == "Player") {
            player.subCurrentHealth(100);
        }
    }
}
